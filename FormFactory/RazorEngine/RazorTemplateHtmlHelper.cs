using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using FormFactory.ViewHelpers;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;

namespace FormFactory.RazorEngine
{
    public class RazorTemplateHtmlHelper : FfHtmlHelper<IDictionary<string, object>> 
    {
        static RazorTemplateHtmlHelper()
        {
            var templateConfig = new TemplateServiceConfiguration
                {
                    Resolver = new DelegateTemplateResolver(name =>
                        {
                            string resourcePath = EmbeddedResourceRegistry.ResolveResourcePath(name);
                            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
                            using (var reader = new StreamReader(stream))
                            {
                                var inherits = "";
                                var sb = new StringBuilder();
                                while (reader.Peek()> 0)
                                {
                                    var readLine = reader.ReadLine();
                                    if (readLine.StartsWith("@model "))
                                    {
                                        inherits = ("@inherits FormFactory.RazorEngine.RazorTemplateFormFactoryTemplate<" + readLine.Substring(7) + ">\r\n");
                                        continue;
                                    }
                                    if (readLine == "@using FormFactory.AspMvc")
                                    {
                                        sb.AppendLine("@using FormFactory.RazorEngine");
                                        continue;
                                    }
                                    sb.AppendLine(readLine);
                                }
                                return inherits + sb.ToString();
                            }
                        })
                };
            templateConfig.BaseTemplateType = typeof (RazorTemplateFormFactoryTemplate<>);
            Razor.SetTemplateService(new TemplateService(templateConfig));
        }


        public IEnumerable<PropertyVm> PropertiesFor(object model, Type type)
        {
            return VmHelper.GetPropertyVmsUsingReflection(this, model, type);
        }
       
      
        public UrlHelper Url()
        {
            throw new NotImplementedException("Url not implemented in FormFactory.RazorTemplate");
        }

        public string WriteTypeToString(Type type)
        {
            return type.AssemblyQualifiedName;
        }

        public ViewData ViewData { get { return new RazorTemplateViewData(this); } }
        public FfContext FfContext
        {
            get { return new RazorEngineContext(this); }
        }

        public object Model { get; set; }

        public IHtmlString BestProperty(PropertyVm vm)
        {
            try
            {
                var viewname = this.FfContext.BestViewName(vm.Type, "FormFactory/Property.");
                viewname = viewname ??
                           FfContext.BestViewName(vm.Type.GetEnumerableType(), "FormFactory/Property.IEnumerable.");
                viewname = viewname ?? "FormFactory/Property.System.Object";
                //must be some unknown object exposed as an interface
                return Partial(viewname, vm);
            }
            catch(Exception ex)
            {
                return new HtmlString(ex.Message);
            }
        }
        public bool HasErrors(string modelName)
        {
            return false;
        }
        public string AllValidationMessages(string x)
        {
            return string.Empty;
        }
        public PropertyVm PropertyVm(Type type, string name, object value)
        {
            return new PropertyVm(this, type, name) { Value = value };
        }

        public IHtmlString Partial(string partialName, object model)
        {
            return Partial(partialName, model, null);
        }
        public IHtmlString Partial(string partialName, object model, IDictionary<string, object> viewData)
        {
            var template = Razor.Resolve(partialName, model);
            var dyn = (dynamic) template;
            dyn.Html = this;
            dyn.Model = (dynamic) model;
            if (viewData != null) dyn.ViewData = viewData;
            try
            {
                string result = template.Run(new ExecuteContext());
                return new HtmlString(result);
            }
            catch (Exception ex)
            {
                return new HtmlString(ex.Message);
            }
        }
         
        public void RenderPartial(string partialName, object model)
        {
            throw new NotImplementedException("RenderPartial is not implemented as there is no context to write to in RazorEngine");
        }

        public PropertyVm CreatePropertyVm(Type objectType, string name)
        {
            return new PropertyVm(this, objectType, name);
        }

        public IHtmlString Raw(object s)
        {
            return new HtmlString(s.ToString()); ;
        }
        //public IHtmlString Raw(IHtmlString s)
        //{
        //    return s ;
        //}
        public ObjectChoices[] Choices(PropertyVm model) //why is this needed? HM
        {
            var html = this;
            var choices = (from obj in model.Choices.Cast<object>().ToArray()
                           let choiceType = obj == null ? model.Type : obj.GetType()
                           let properties = html.PropertiesFor(obj, choiceType)
                               .Each(p => p.Name = model.Name + "." + p.Name)
                               .Each(p => p.Readonly |= model.Readonly)
                               .Each(p => p.Id = Guid.NewGuid().ToString())
                           select new ObjectChoices { obj = obj, choiceType = choiceType, properties = properties, name = (obj != null ? obj.DisplayName() : choiceType.DisplayName()) }).ToArray();
            return choices;
        }

    }

    
}