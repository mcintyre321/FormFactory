using System.Linq;
using FormFactory.Attributes;
using FormFactory.Standalone;

namespace FormFactory
{
    public static class AspMvcViewHelper
    {
        public static MvcHtmlString Raw(this bool value, string output)
        {
            return new MvcHtmlString(value ? output : "");
        }
        public static MvcHtmlString Raw(this string value)
        {
            return new MvcHtmlString(value);
        }

        public static MvcHtmlString InputAtts(this PropertyVm vm)
        {
            return new MvcHtmlString(string.Join("", string.Join(" ", new string[] { vm.Disabled().ToString(), vm.Readonly().ToString(), vm.DataAtts().ToString() })));
        }
        public static MvcHtmlString Disabled(this PropertyVm vm)
        {
            return Attr(vm.Disabled, "disabled", null);
        }
        public static MvcHtmlString Readonly(this PropertyVm vm)
        {
            return Attr(vm.Readonly, "readonly", null);
        }


        public static MvcHtmlString Attr(this bool value, string att, string attValue = null)
        {
            return value.Raw(att + "=\"" + (attValue ?? att).Replace("\"", "&quot;") + "\"");
        }
        public static MvcHtmlString Attr(this string value, string att)
        {
            if (value == null) return new MvcHtmlString("");
            return Raw(att + "=\"" + (value ?? att).Replace("\"", "&quot;") + "\"");
        }

        public static MvcHtmlString Placeholder(PropertyVm pi)
        {
            var placeHolderText = pi.GetCustomAttributes().OfType<DisplayAttribute>().Select(a => a.Prompt).FirstOrDefault();
            return Attr((!string.IsNullOrWhiteSpace(placeHolderText)), "placeholder", placeHolderText);
        }
    }
}