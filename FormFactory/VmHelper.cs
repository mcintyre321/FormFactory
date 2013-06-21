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
         

        public static IEnumerable<PropertyVm<THelper>> PropertiesFor<THelper>(THelper helper, object model, Type fallbackModelType = null)
            where THelper : FfHtmlHelper
        {
            fallbackModelType = fallbackModelType ?? model.GetType();
            return VmHelper<THelper>.GetPropertyVms(helper, model, fallbackModelType).Cast<PropertyVm<THelper>>();
        }


        public static IEnumerable<PropertyVm<THelper>> GetPropertyVmsUsingReflection<THelper>(THelper helper, object model, Type fallbackModelType)
            where THelper : FfHtmlHelper
        {
            var type = model != null ? model.GetType() : fallbackModelType;

            var typeVm = new PropertyVm<THelper>(helper, typeof(string), "__type")
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

                var inputVm = new PropertyVm<THelper>(model, property, helper);
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

        public static string Render(IEnumerable<PropertyVm> properties)
        {
            var sb = new StringBuilder();
            foreach (var propertyVm in properties)
            {
                sb.Append(propertyVm.Html.Partial("FormFactory/Form.Property", propertyVm));
            }
            return (sb.ToString());
        }

        public static string Render(PropertyVm propertyVm)
        {
            return propertyVm.Html.Partial("FormFactory/Form.Property", propertyVm);
        }

        public static string ToHtmlString(IEnumerable<PropertyVm> properties)
        {
            var sb = new StringBuilder();
            foreach (var propertyVm in properties)
            {
                sb.AppendLine(propertyVm.Html.Partial("FormFactory/Form.Property", propertyVm));
            }
            var htmlString = (sb.ToString());
            return htmlString;
        }
    }
}