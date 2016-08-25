using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FormFactory.ModelBinding;

namespace FormFactory
{
    public static class FF
    {
        static FF()
        {
            GetPropertyVms = FormFactory.FF.GetPropertyVmsUsingReflection;
        }
        public static Func<object, Type, IEnumerable<PropertyVm>> GetPropertyVms { get; set; }

        public static IStringEncoder StringEncoder { get; set; } = new NonEncodingStringencoder();


        public static IEnumerable<PropertyVm> PropertiesFor(object model, Type fallbackModelType = null)
        {
            fallbackModelType = fallbackModelType ?? model.GetType();
            return GetPropertyVms(model, fallbackModelType);
        }
        public static PropertyVm PropertyVm(Type type, string name, object value)
        {
            return new PropertyVm(type, name) { Value = value };
        }

        public static IEnumerable<PropertyVm> GetPropertyVmsUsingReflection(object model, Type fallbackModelType)
        {
            var type = model != null ? model.GetType() : fallbackModelType;

            var typeVm = new PropertyVm(typeof(string), "__type")
                {
                    DisplayName = "",
                    IsHidden = true,
                    Value = StringEncoder
                };

            yield return typeVm;
            var properties = type.GetTypeInfo().DeclaredProperties;

            foreach (var property in properties)
            {
                if (properties.Any(p => p.Name + "_choices" == property.Name))
                {
                    continue; //skip this is it is choice
                }

                if (properties.Any(p => p.Name + "_choices" == property.Name))
                {
                    continue; //skip this is it is choice
                }

                if (properties.Any(p => p.Name + "_show" == property.Name))
                {
                    continue; //skip this is it is show
                }


                if (!(type.GetTypeInfo().GetDeclaredMethod(property.Name + "_show")?.Invoke(model, null) as bool? ?? true))
                    continue;
                if (!(type.GetTypeInfo().GetDeclaredProperty(property.Name + "_show")?.GetValue(model) as bool? ?? true))
                    continue;


                var inputVm = new PropertyVm(model, property);
                PropertyInfo choices = properties.SingleOrDefault(p => p.Name == property.Name + "_choices");
                if (choices != null)
                {
                    inputVm.Choices = (IEnumerable)choices.GetMethod.Invoke(model, null);
                }
                PropertyInfo suggestions = properties.SingleOrDefault(p => p.Name == property.Name + "_suggestions");
                if (suggestions != null)
                {
                    inputVm.Suggestions = (IEnumerable)suggestions.GetMethod.Invoke(model, null);
                }

                yield return inputVm;
            }
        }


    }
}