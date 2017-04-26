using System;
using System.Collections;
using System.Collections.Generic;
using FormFactory.Attributes;
using System.Linq;
using System.Reflection;

namespace FormFactory
{


    public class PropertyVm : IHasDisplayName
    {
        static PropertyVm()
        {
        }

        public PropertyVm()
        {
            
        }

        public PropertyVm(ParameterInfo pi)
            : this(pi.ParameterType, pi.Name)
        {
            Readonly = !true;
            IsHidden = pi.GetCustomAttributes(true).OfType<HiddenAttribute>().Any();
            GetCustomAttributes = () => pi.GetCustomAttributes(true);

            var descriptionAttr = pi.GetCustomAttributes(true).OfType<FormFactory.Attributes.DisplayAttribute>()
                .FirstOrDefault();
            DisplayName = descriptionAttr != null ? descriptionAttr.Name : pi.Name.Sentencise();
        }
        public PropertyVm(ParameterInfo modelParamInfo, PropertyInfo pi)
            : this(pi.PropertyType, pi.Name)
        {
            Readonly = !true;
            IsHidden = pi.GetCustomAttributes(true).OfType<HiddenAttribute>().Any();
            GetCustomAttributes = () => pi.GetCustomAttributes(true);

            var descriptionAttr = pi.GetCustomAttributes(true).OfType<DisplayAttribute>()
                .FirstOrDefault();
            DisplayName = descriptionAttr != null ? descriptionAttr.Name : pi.Name.Sentencise();


        }

        public IDictionary<string, string> DataAttributes { get; set; }
        public object Source { get; set; }
        public PropertyVm(object model, PropertyInfo pi) :
            this(pi.PropertyType, pi.Name)
        {
            Source = model;
            var getMethod = pi.GetMethod;
            
            if (pi.GetIndexParameters().Any()) getMethod = null; //dont want to get indexed properties

            //if (html.ViewData.ModelState.TryGetValue(pi.Name, out modelState))
            //{
            //    if (modelState.Value != null)
            //        Value = modelState.Value.AttemptedValue;
            //}
            //else 
                if (getMethod != null && model != null)
            {
                Value = getMethod.Invoke(model, null);
            }
            if (model != null)
            {
                MethodInfo choices = model.GetType().GetTypeInfo().GetDeclaredMethod(pi.Name + "_choices");
                if (choices != null)
                {
                    Choices = (IEnumerable)choices.Invoke(model, null);
                }
                MethodInfo suggestions = model.GetType().GetTypeInfo().GetDeclaredMethod(pi.Name + "_suggestions");
                if (suggestions != null)
                {
                    Suggestions = (IEnumerable)suggestions.Invoke(model, null);
                }
                var setter = pi.SetMethod;
                var getter = getMethod;
                Readonly = setter == null;
                Value = getter?.Invoke(model, null);
            }
            GetCustomAttributes = () => pi.GetCustomAttributes(true);
            Readonly = pi.SetMethod == null;
            IsHidden = pi.GetCustomAttributes(true).OfType<HiddenAttribute>().Any();

            var descriptionAttr = pi.GetCustomAttributes(true).OfType<DisplayAttribute>()
                .FirstOrDefault();
            DisplayName = descriptionAttr != null ? descriptionAttr.Name : pi.Name.Sentencise();
            DataAttributes = new Dictionary<string, string>();
        }


        public bool? NotOptional { get; set; }

        public PropertyVm(Type type, string name)
        {
            Type = type;
            Name = name;
            Id = Guid.NewGuid().ToString();

            //if (html.ViewData.ModelState.TryGetValue(name, out modelState))
            //{
            //    if (modelState.Value != null)
            //        Value = modelState.Value.AttemptedValue;
            //}
            GetCustomAttributes = () => new object[] { };
        }

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
    }
    
    public static class Extensions
    {
        public static U Maybe<T, U>(this T t, Func<T, U> f) where T : class
        {
            return (t == null) ? default(U) : f(t);
        }
        public static bool HasAttribute<TAtt>(this PropertyVm propertyVm)
        {
            return propertyVm.GetCustomAttributes().OfType<TAtt>().Any();
        }
    }
}