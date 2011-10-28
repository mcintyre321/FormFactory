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
            this(html, pi.PropertyType, pi.Name, displayName)
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

        public PropertyVm(HtmlHelper html, Type type, string name, string displayName = null)
        {
            Type = type;
            Name = name;
            DisplayName = displayName ?? Name.Sentencise();
            ModelState modelState;
            if (html.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                if (modelState.Value != null)
                    Value = modelState.Value.AttemptedValue;
            }
        }

        public Type Type { get; private set; }

        public string Name { get; private set; }
        public string DisplayName { get; private set; }

        public object Value { get; private set; }


        public Func<IEnumerable<object>> GetCustomAttributes { get; set; }

        public bool IsWritable { get; internal set; }

        public IEnumerable Choices { get; set; }
    }
}