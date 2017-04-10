using FormFactory.Attributes;
using System.Linq;
using FormFactory.Attributes;
using RazorLight.Text;

namespace FormFactory.Standalone
{
    public static class RazorViewHelper
    {
        public static RawString Raw(this bool value, string output)
        {
            return new RawString(value ? output : "");
        }
        public static RawString Raw(this string value)
        {
            return new RawString(value);
        }

        public static RawString InputAtts(this PropertyVm vm)
        {
            return new RawString(string.Join("", string.Join(" ", new string[] { vm.Disabled().ToString(), vm.Readonly().ToString(), vm.DataAtts().ToString() })));
        }
        public static RawString Disabled(this PropertyVm vm)
        {
            return Attr(vm.Disabled, "disabled", null);
        }
        public static RawString Readonly(this PropertyVm vm)
        {
            return Attr(vm.Readonly, "readonly", null);
        }


        public static RawString Attr(this bool value, string att, string attValue = null)
        {
            return value.Raw(att + "=\"" + (attValue ?? att).Replace("\"", "&quot;") + "\"");
        }
        public static RawString Attr(this string value, string att)
        {
            if (value == null) return new RawString("");
            return Raw(att + "=\"" + (value ?? att).Replace("\"", "&quot;") + "\"");
        }

        public static RawString Placeholder(PropertyVm pi)
        {
            var placeHolderText = pi.GetCustomAttributes().OfType<DisplayAttribute>().Select(a => a.Prompt).FirstOrDefault();
            return Attr((!string.IsNullOrWhiteSpace(placeHolderText)), "placeholder", placeHolderText);
        }
    }
}