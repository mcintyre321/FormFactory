using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using FormFactory.Attributes;

namespace FormFactory
{
    public static class ViewHelper
    {
        public static string Raw(this bool value, string output)
        {
            return (value ? output : "");
        }
        public static string Raw(this string value)
        {
            return (value);
        }

        public static string InputAtts(this PropertyVm vm)
        {
            return string.Join("", string.Join(" ", new string[] { vm.Disabled(), vm.Readonly(), vm.DataAtts() }));
        }
        public static string Disabled(this PropertyVm vm)
        {
            return Attr(vm.Disabled, "disabled", null);
        }
        public static string Readonly(this PropertyVm vm)
        {
            return Attr(vm.Readonly, "readonly", null);
        }
        public static string DataAtts(this PropertyVm vm)
        {
            var sb = new StringBuilder();
            foreach (var att in vm.DataAttributes)
            {
                sb.Append("data-" + att.Key + "='" + att.Value + "' ");
            }
            return (sb.ToString());
        }

        public static string Attr(this bool value, string att, string attValue = null)
        {
            return value.Raw(att + "=\"" + (attValue ?? att) + "\"");
        }
        public static string Attr(this string value, string att)
        {
            if (value == null) return ("");
            return Raw(att + "=\"" + (value ?? att) + "\"");
        }

        public static string Placeholder(PropertyVm pi)
        {
            var placeHolderText = pi.GetCustomAttributes().OfType<PlaceholderAttribute>().Select(a => a.Text).FirstOrDefault();
            return Attr((!string.IsNullOrWhiteSpace(placeHolderText)), "placeholder", placeHolderText);
        }
    }

    
     
}
