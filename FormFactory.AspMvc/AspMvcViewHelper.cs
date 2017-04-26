using FormFactory.Attributes;
using System.Linq;
using System.Web;

namespace FormFactory.AspMvc
{
    public static class AspMvcViewHelper
    {
        public static IHtmlString Raw(this bool value, string output)
        {
            return new HtmlString(value ? output : "");
        }
        public static IHtmlString Raw(this string value)
        {
            return new HtmlString(value);
        }

        public static IHtmlString InputAtts(this PropertyVm vm)
        {
            return new HtmlString(string.Join("", string.Join(" ", new string[] { vm.Disabled().ToString(), vm.Readonly().ToString(), vm.DataAtts().ToString() })));
        }
        public static IHtmlString Disabled(this PropertyVm vm)
        {
            return Attr(vm.Disabled, "disabled", null);
        }
        public static IHtmlString Readonly(this PropertyVm vm)
        {
            return Attr(vm.Readonly, "readonly", null);
        }


        public static IHtmlString Attr(this bool value, string att, string attValue = null)
        {
            return value.Raw(att + "=\"" + (attValue ?? att).Replace("\"", "&quot;") + "\"");
        }
        public static IHtmlString Attr(this string value, string att)
        {
            if (value == null) return new HtmlString("");
            return Raw(att + "=\"" + (value ?? att).Replace("\"", "&quot;") + "\"");
        }

        public static IHtmlString Placeholder(PropertyVm pi)
        {
            var placeHolderText = pi.GetCustomAttributes().OfType<DisplayAttribute>().Select(a => a.Prompt).FirstOrDefault();
            return Attr((!string.IsNullOrWhiteSpace(placeHolderText)), "placeholder", placeHolderText);
        }
    }
}