using FormFactory.Attributes;
using System.Linq;
using FormFactory.AspMvc.Wrappers;
using Microsoft.AspNetCore.Html;

namespace FormFactory
{
    public static class AspMvcViewHelper
    {
        public static IHtmlContent Raw(this bool value, string output)
        {
            return new HtmlString(value ? output : "");
        }
        public static IHtmlContent Raw(this string value)
        {
            return new HtmlString(value);
        }

        public static IHtmlContent InputAtts(this PropertyVm vm)
        {
            return new HtmlString(string.Join("", string.Join(" ", new string[] { vm.Disabled().ToString(), vm.Readonly().ToString(), vm.DataAtts().ToString() })));
        }
        public static IHtmlContent Disabled(this PropertyVm vm)
        {
            return Attr(vm.Disabled, "disabled", null);
        }
        public static IHtmlContent Readonly(this PropertyVm vm)
        {
            return Attr(vm.Readonly, "readonly", null);
        }


        public static IHtmlContent Attr(this bool value, string att, string attValue = null)
        {
            return value.Raw(att + "=\"" + (attValue ?? att).Replace("\"", "&quot;") + "\"");
        }
        public static IHtmlContent Attr(this string value, string att)
        {
            if (value == null) return new HtmlString("");
            return Raw(att + "=\"" + (value ?? att).Replace("\"", "&quot;") + "\"");
        }

        public static IHtmlContent Placeholder(PropertyVm pi)
        {
            var placeHolderText = pi.GetCustomAttributes().OfType<DisplayAttribute>().Select(a => a.Prompt).FirstOrDefault();
            return Attr((!string.IsNullOrWhiteSpace(placeHolderText)), "placeholder", placeHolderText);
        }
    }
}