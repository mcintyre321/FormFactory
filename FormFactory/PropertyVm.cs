using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;

namespace FormFactory
{
    public class PropertyVm
    {
        public PropertyVm(ParameterInfo pi, HtmlHelper html) : this(html, pi.ParameterType, pi.Name)
        {
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(pi.Name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            IsWritable = true;
            GetCustomAttributes = () => pi.GetCustomAttributes(true);
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
            IsWritable = true;
            GetCustomAttributes = () => pi.GetCustomAttributes(true);
        }

        public object Source { get; set; }
        public PropertyVm(object model, PropertyInfo property, HtmlHelper html) :
            this(html, property.PropertyType, property.Name)
        {
            Source = model;
            ModelState modelState;
            var type = property.PropertyType;
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
                IsWritable = setter != null;
                Value = getter == null ? null : getter.Invoke(model, null);
            }
            GetCustomAttributes = () => property.GetCustomAttributes(true);
            IsWritable = property.GetSetMethod() != null;
        }

        public PropertyVm(HtmlHelper html, Type type, string name)
        {
            Type = type;
            Name = name;
            Id = Name;
            DisplayName = Name.Sentencise();
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            Html = html;
            GetCustomAttributes = () => new object[]{};
        }

        protected internal HtmlHelper Html { get; set; }

        public string Id { get; set; }

        public Type Type { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public object Value { get; set; }


        public Func<IEnumerable<object>> GetCustomAttributes { get; set; }

        public bool IsWritable { get; set; }

        public IEnumerable Choices { get; set; }
        public IEnumerable Suggestions { get; set; }

    }
}