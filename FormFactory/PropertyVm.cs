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

        public PropertyVm(object o, PropertyInfo pi, HtmlHelper html, string displayName = null) :
            this(html, pi.PropertyType, pi.Name, null, displayName)
        {
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(pi.Name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
            else if (pi.GetGetMethod() != null)
            {
                Value = pi.GetGetMethod().Invoke(o, null);
            }
            GetCustomAttributes = () => pi.GetCustomAttributes(true);
            IsWritable = pi.GetSetMethod() != null;
        }

        public PropertyVm(HtmlHelper html, Type type, string name, string id = null, string displayName = null)
        {
            Type = type;
            Name = name;
            Id = id ?? Name;
            DisplayName = displayName ?? Name.Sentencise();
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
        }

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