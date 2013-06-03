using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using FormFactory.Attributes;

namespace FormFactory
{
    public class PropertyVm : IHasDisplayName
    {
        static PropertyVm()
        {
        }

        public PropertyVm(ParameterInfo pi, FfHtmlHelper html)
            : this(html, pi.ParameterType, pi.Name)
        {
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(pi.Name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            Readonly = !true;
            IsHidden = pi.GetCustomAttributes(true).OfType<DataTypeAttribute>()
                .Any(x => x.CustomDataType == "Hidden");
            ShowLabel = pi.GetCustomAttributes(true).OfType<NoLabelAttribute>().Any() == false;
            GetCustomAttributes = () => pi.GetCustomAttributes(true);

            var descriptionAttr = pi.GetCustomAttributes(true).OfType<DisplayAttribute>()
                .FirstOrDefault(x => !string.IsNullOrEmpty(x.Name));
            DisplayName = descriptionAttr != null ? descriptionAttr.Name : pi.Name.Sentencise();

             
        }
        public PropertyVm(ParameterInfo modelParamInfo, PropertyInfo pi, FfHtmlHelper html)
            : this(html, pi.PropertyType, pi.Name)
        {
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(modelParamInfo.Name + "." + pi.Name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            else if (html.ViewData.Model != null && (html.ViewData.Model.GetType() == modelParamInfo.ParameterType))
            {
                Value = pi.GetValue(html.ViewData.Model, new object[0]);
            }
            Readonly = !true;
            IsHidden = pi.GetCustomAttributes(true).OfType<DataTypeAttribute>().Any(x => x.CustomDataType == "Hidden");
            ShowLabel = pi.GetCustomAttributes(true).OfType<NoLabelAttribute>().Any() == false;
            GetCustomAttributes = () => pi.GetCustomAttributes(true);

            var descriptionAttr = pi.GetCustomAttributes(true).OfType<DisplayAttribute>()
                .FirstOrDefault(x => !string.IsNullOrEmpty(x.Name));
            DisplayName = descriptionAttr != null ? descriptionAttr.Name : pi.Name.Sentencise();

             
        }

        public IDictionary<string, string> DataAttributes { get; private set; }
        public object Source { get; set; }
        public PropertyVm(object model, PropertyInfo pi, FfHtmlHelper html) :
            this(html, pi.PropertyType, pi.Name)
        {
            Source = model;
            ModelState modelState;
            var getMethod = pi.GetGetMethod();
            
            if (pi.GetIndexParameters().Any()) getMethod = null; //dont want to get indexed properties

            if (html.ViewData.ModelState.TryGetValue(pi.Name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            else if (getMethod != null && model != null)
            {
                Value = getMethod.Invoke(model, null);
            }
            if (model != null)
            {
                MethodInfo choices = model.GetType().GetMethod(pi.Name + "_choices");
                if (choices != null)
                {
                    Choices = (IEnumerable)choices.Invoke(model, null);
                }
                MethodInfo suggestions = model.GetType().GetMethod(pi.Name + "_suggestions");
                if (suggestions != null)
                {
                    Suggestions = (IEnumerable)suggestions.Invoke(model, null);
                }
                var setter = pi.GetSetMethod();
                var getter = getMethod;
                Readonly = !(setter != null);
                Value = getter == null ? null : getter.Invoke(model, null);
            }
            GetCustomAttributes = () => pi.GetCustomAttributes(true);
            Readonly = !(pi.GetSetMethod() != null);
            IsHidden = pi.GetCustomAttributes(true).OfType<DataTypeAttribute>()
                .Any(x => x.CustomDataType == "Hidden");
            ShowLabel = pi.GetCustomAttributes(true).OfType<NoLabelAttribute>().Any() == false;

            var descriptionAttr = pi.GetCustomAttributes(true).OfType<DisplayAttribute>()
                .FirstOrDefault(x => !string.IsNullOrEmpty(x.Name));
            DisplayName = descriptionAttr != null ? descriptionAttr.Name : pi.Name.Sentencise();
            DataAttributes = new Dictionary<string, string>();
        }

        public PropertyVm(FfHtmlHelper html, Type type, string name)
        {
            Type = type;
            Name = name;
            Id = Guid.NewGuid().ToString();

            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            Html = html;
            GetCustomAttributes = () => new object[] { };
            ShowLabel = true;
        }

        protected internal FfHtmlHelper Html { get; set; }

        public string Id { get; set; }
        public Type Type { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public object Value { get; set; }


        public Func<IEnumerable<object>> GetCustomAttributes { get; set; }
        public T GetAttribute<T>()
        {
            return GetCustomAttributes().OfType<T>().SingleOrDefault();
        }

        public bool Readonly { get; set; }
        public bool Disabled { get; set; }


        public IEnumerable Choices { get; set; }
        public IEnumerable Suggestions { get; set; }

        public bool IsHidden { get; set; }
        public bool ShowLabel { get; set; }
    }
    
    static class Extensions
    {
        public static U Maybe<T, U>(this T t, Func<T, U> f) where T : class
        {
            return (t == null) ? default(U) : f(t);
        }
    }
}