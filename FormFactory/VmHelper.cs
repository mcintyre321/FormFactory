using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FormFactory.ModelBinding;

namespace FormFactory
{
    public static class VmHelper
    {
        static VmHelper()
        {
            GetPropertyVms = FormFactory.VmHelper.GetPropertyVmsUsingReflection;
        }
        public static Func<IStringEncoder, object, Type, IEnumerable<PropertyVm>> GetPropertyVms { get; set; }



        public static IEnumerable<PropertyVm> PropertiesFor(IStringEncoder helper, object model, Type fallbackModelType = null)
        {
            fallbackModelType = fallbackModelType ?? model.GetType();
            return GetPropertyVms(helper, model, fallbackModelType);
        }


        public static IEnumerable<PropertyVm> GetPropertyVmsUsingReflection(IStringEncoder helper, object model, Type fallbackModelType)
        {
            var type = model != null ? model.GetType() : fallbackModelType;

            var typeVm = new PropertyVm(typeof(string), "__type")
                {
                    DisplayName = "",
                    IsHidden = true,
                    Value = helper.WriteTypeToString(type)
                };

            yield return typeVm;
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                if (properties.Any(p => p.Name + "_choices" == property.Name))
                {
                    continue; //skip this is it is choice
                }

                var inputVm = new PropertyVm(model, property);
                PropertyInfo choices = properties.SingleOrDefault(p => p.Name == property.Name + "_choices");
                if (choices != null)
                {
                    inputVm.Choices = (IEnumerable)choices.GetGetMethod().Invoke(model, null);
                }
                PropertyInfo suggestions = properties.SingleOrDefault(p => p.Name == property.Name + "_suggestions");
                if (suggestions != null)
                {
                    inputVm.Suggestions = (IEnumerable)suggestions.GetGetMethod().Invoke(model, null);
                }

                yield return inputVm;
            }
        }


    }
}