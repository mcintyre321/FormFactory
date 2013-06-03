using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using FormFactory.AspMvc;
using FormFactory.AspMvc.Wrappers;

namespace FormFactory.AspMvc
{
    public static class VmHelper
    {
        
        public static IEnumerable<PropertyVm> PropertiesFor<T>(this System.Web.Mvc.HtmlHelper helper, T model)
        {
            return helper.PropertiesFor(model, typeof(T));
        }
       
        public static IEnumerable<PropertyVm> PropertiesFor(this System.Web.Mvc.HtmlHelper helper, object model, Type fallbackModelType)
        {
            return new FormFactoryHtmlHelper(helper).PropertiesFor(model, fallbackModelType);
        }
        public static PropertyVm PropertyVm(this HtmlHelper html, Type type, string name, object value)
        {
            return new PropertyVm(new FormFactoryHtmlHelper(html), type, name ){ Value = value};
        }

        //public static System.Web.IHtmlString UnobtrusiveValidation(HtmlHelper html, PropertyVm model)
        //{
        //    return new System.Web.HtmlString(new FormFactoryHtmlHelper(html).UnobtrusiveValidation(model).ToEncodedString());
        //}
        //private static IEnumerable<PropertyVm> GetPropertyVmsUsingReflection(HtmlHelper helper, object model, Type fallbackModelType)
        //{
        //    var type = model != null ? model.GetType() : fallbackModelType;

        //    var typeVm = new PropertyVm(helper, typeof (string), "__type");
        //    typeVm.IsHidden = true;

        //    typeVm.Value = helper.WriteTypeToString(type);
        //    yield return typeVm;
        //    var properties = type.GetProperties();

        //    foreach (var property in properties)
        //    {
        //        if (properties.Any(p => p.Name + "Choices" == property.Name))
        //        {
        //            continue; //skip this is it is choice
        //        }

        //        var inputVm = new PropertyVm(model, property, helper);
        //        PropertyInfo choices = properties.SingleOrDefault(p => p.Name == property.Name + "_choices");
        //        if (choices != null)
        //        {
        //            inputVm.Choices = (IEnumerable) choices.GetGetMethod().Invoke(model, null);
        //        }
        //        PropertyInfo suggestions = properties.SingleOrDefault(p => p.Name == property.Name + "_suggestions");
        //        if (suggestions != null)
        //        {
        //            inputVm.Suggestions = (IEnumerable) suggestions.GetGetMethod().Invoke(model, null);
        //        }

        //        yield return inputVm;
        //    }
        //}

        //public static void Render(this IEnumerable<PropertyVm> properties)
        //{
        //    foreach (var propertyVm in properties)
        //    {
        //        propertyVm.Html.RenderPartial("FormFactory/Form.Property", propertyVm);
        //    }
        //}
        //public static HtmlString ToHtmlString(this IEnumerable<PropertyVm> properties)
        //{
        //    var sb = new StringBuilder();
        //    foreach (var propertyVm in properties)
        //    {
        //        sb.AppendLine(propertyVm.Html.Partial("FormFactory/Form.Property", propertyVm).ToHtmlString());
        //    }
        //    var HtmlString = new HtmlString(sb.ToString());
        //    return HtmlString;

        //}
    }
}