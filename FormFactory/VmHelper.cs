using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FormFactory
{
    public static class VmHelper<THelper> where THelper : FfHtmlHelper
    {
        static VmHelper()
        {
            GetPropertyVms = FormFactory.VmHelper.GetPropertyVmsUsingReflection;
        }
        public static Func<THelper, object, Type, IEnumerable<PropertyVm>> GetPropertyVms { get; set; }
    }
    public static class VmHelper
    {
         

        public static IEnumerable<PropertyVm> PropertiesFor<THelper>( THelper helper, object model, Type fallbackModelType = null)
            where THelper : FfHtmlHelper
        {
            fallbackModelType = fallbackModelType ?? model.GetType();
            return VmHelper<THelper>.GetPropertyVms(helper, model, fallbackModelType).Cast<PropertyVm>();
        }


        public static IEnumerable<PropertyVm> GetPropertyVmsUsingReflection<THelper>(THelper helper, object model, Type fallbackModelType)
            where THelper : FfHtmlHelper
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
                if (properties.Any(p => p.Name + "Choices" == property.Name))
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