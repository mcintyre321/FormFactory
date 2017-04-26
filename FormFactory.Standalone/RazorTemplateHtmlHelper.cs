using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FormFactory.ViewHelpers;
using RazorLight;
using RazorLight.Text;

namespace FormFactory.Standalone
{
    public class RazorTemplateHtmlHelper : FfHtmlHelper<IDictionary<string, object>> 
    {
        
         

 

        public RawString UnobtrusiveValidation(PropertyVm property)
        {
            return new RawString(ValidationHelper.UnobtrusiveValidation(this, property));
        }

        public RawString AntiForgeryToken()
        {
            return new RawString("");
        }


        public UrlHelper Url()
        {
            throw new NotImplementedException("Url not implemented in FormFactory.RazorTemplate");
        }

        public string WriteTypeToString(Type type)
        {
            return type.AssemblyQualifiedName;
        }

        public ViewData ViewData => new ViewData(new FfModelStateDictionary(), this.Model);

        public IViewFinder ViewFinder => new RazorLightContext();

        public object Model { get; set; }

        public RawString BestProperty(PropertyVm vm)
        {
            try
            {
                var viewname = ViewFinderExtensions.BestViewName(this.ViewFinder, vm.Type, "FormFactory/Property.");
                viewname = viewname ??
                           ViewFinderExtensions.BestViewName(ViewFinder, vm.Type.GetEnumerableType(), "FormFactory/Property.IEnumerable.");
                viewname = viewname ?? "FormFactory/Property.System.Object";
                //must be some unknown object exposed as an interface
                return Partial(viewname, vm);
            }
            catch(Exception ex)
            {
                return new RawString(ex.Message);
            }
        }

        public RawString ValidationSummary(bool excludeFieldErrors)
        {
            return new RawString("");
        }

        public bool HasErrors(string modelName)
        {
            return false;
        }
        public string AllValidationMessages(string x)
        {
            return string.Empty;
        }
       

        public RawString Partial(string partialName, object model, ViewData viewData = null)
        {
            var engine = EngineFactory.CreateEmbedded(typeof(FF));


            var resourceName = "Views.Shared." + partialName.Replace("/", ".");
            var str = engine.Parse(resourceName, model: model, viewBag: null, prerenderCallback: new Action<TemplatePage>((t) =>
            {
                var templateBase = (IFormFactoryTemplatePage)t;
                templateBase.Html = new RazorTemplateHtmlHelper();
                templateBase.ViewData = viewData ?? new ViewData();
            }));
            return new RawString(str);
        }
         
        public void RenderPartial(string partialName, object model)
        {
            throw new NotImplementedException("RenderPartial is not implemented as there is no context to write to in RazorLight");
        }


        public PropertyVm CreatePropertyVm(Type objectType, string name)
        {
            return new PropertyVm(objectType, name);
        }

        public RawString Raw(object s)
        {
            return new RawString(s.ToString()); ;
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
                           let properties = FF.PropertiesFor(obj, choiceType)
                               .Each(p => p.Name = model.Name + "." + p.Name)
                               .Each(p => p.Readonly |= model.Readonly)
                               .Each(p => p.Id = Guid.NewGuid().ToString())
                           select new ObjectChoices { obj = obj, choiceType = choiceType, properties = properties, name = (obj != null ? obj.DisplayName() : choiceType.DisplayName()) }).ToArray();
            return choices;
        }
       

        public RawString BestPartial(object model, Type type = null, string prefix = null)
        {
            return Partial(ViewFinderExtensions.BestPartialName(this, model, type, prefix), model);
        }


        //public static RawString Render<THelper>(IEnumerable<PropertyVm<THelper>> properties) where THelper : FfHtmlHelper
        //{
        //    return (Render(properties));
        //}

        //public static RawString Render<THelper>(PropertyVm<THelper> propertyVm) where THelper : FfHtmlHelper
        //{
        //    return (Render(propertyVm));
        //}
        
        //string Partial(string partialName, object vm); 
        //string Partial(string partialName, object vm, TViewData viewData);

        public static RawString Render(PropertyVm propertyVm, RazorTemplateHtmlHelper html)
        {
            return html.Partial("FormFactory/Form.Property", propertyVm);
        }

        public static string ToHtmlString(IEnumerable<PropertyVm> properties, RazorTemplateHtmlHelper html)
        {
            var sb = new StringBuilder();
            foreach (var propertyVm in properties)
            {
                sb.AppendLine(html.Partial("FormFactory/Form.Property", propertyVm).ToString());
            }
            var htmlString = (sb.ToString());
            return htmlString;
        }
    }

    
}