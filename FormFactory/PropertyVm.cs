using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using FormFactory.Attributes;

namespace FormFactory
{
    public class PropertyVm
    {
        public PropertyVm(ParameterInfo pi, HtmlHelper html)
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
        public PropertyVm(PropertyInfo pi, HtmlHelper html)
            : this(html, pi.PropertyType, pi.Name)
        {
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(pi.Name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            else if (html.ViewData.Model != null)
            {
                Value = pi.GetValue(html.ViewData.Model, new object[0]);
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

        public object Source { get; set; }
        public PropertyVm(object model, PropertyInfo property, HtmlHelper html) :
            this(html, property.PropertyType, property.Name)
        {
            Source = model;
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(property.Name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            else if (property.GetGetMethod() != null && model != null)
            {
                Value = property.GetGetMethod().Invoke(model, null);
            }
            if (model != null)
            {
                MethodInfo choices = model.GetType().GetMethod(property.Name + "_choices");
                if (choices != null)
                {
                    Choices = (IEnumerable) choices.Invoke(model, null);
                }
                MethodInfo suggestions = model.GetType().GetMethod(property.Name + "_suggestions");
                if (suggestions != null)
                {
                    Suggestions = (IEnumerable) suggestions.Invoke(model, null);
                }
                var setter = property.GetSetMethod();
                var getter = property.GetGetMethod();
                Readonly = !(setter != null);
                Value = getter == null ? null : getter.Invoke(model, null);
            }
            GetCustomAttributes = () => property.GetCustomAttributes(true);
            Readonly = !(property.GetSetMethod() != null);
            IsHidden = property.GetCustomAttributes(true).OfType<DataTypeAttribute>()
                .Any(x => x.CustomDataType == "Hidden");
            ShowLabel = property.GetCustomAttributes(true).OfType<NoLabelAttribute>().Any() == false;

            var descriptionAttr = property.GetCustomAttributes(true).OfType<DisplayAttribute>()
                .FirstOrDefault(x => !string.IsNullOrEmpty(x.Name));
            DisplayName = descriptionAttr != null ? descriptionAttr.Name : property.Name.Sentencise();
        }

        public PropertyVm(HtmlHelper html, Type type, string name)
        {
            Type = type;
            Name = name;
            GetId = () => Name;
            
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            Html = html;
            GetCustomAttributes = () => new object[]{};
            ShowLabel = true;
        }

        protected internal HtmlHelper Html { get; set; }

        public Func<string> GetId { private get; set; }
        public string Id { get { return GetId(); } }
        public Type Type { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public object Value { get; set; }


        public Func<IEnumerable<object>> GetCustomAttributes { get; set; }

        public bool Readonly { get; set; }
        public bool Disabled { get; set; }


        public IEnumerable Choices { get; set; }
        public IEnumerable Suggestions { get; set; }

        public bool IsHidden { get; set; }
        public bool ShowLabel { get; set; }
    }
}