using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FormFactory
{
    public static class ViewHelper
    {
        public static MvcHtmlString Raw(this bool value, string output)
        {
            return new MvcHtmlString(value ? output : "");
        }
        public static IHtmlString InputAtts(this PropertyVm vm)
        {
            return new HtmlString(string.Join(" ", new string[]{vm.Disabled().ToHtmlString(), vm.Readonly().ToHtmlString(), vm.DataAtts().ToHtmlString()}));
        }
        public static IHtmlString Disabled(this PropertyVm vm)
        {
            return vm.Disabled.Att("disabled");
        }
        public static IHtmlString Readonly(this PropertyVm vm)
        {
            return vm.Readonly.Att("readonly");
        }
        public static IHtmlString DataAtts(this PropertyVm vm)
        {
            var sb = new StringBuilder();
            foreach (var att in vm.DataAttributes)
            {
                sb.Append("data-" + att.Key + "='" + att.Value + "' ");
            }
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString Att(this bool value, string att, string attValue = null)
        {
            return value.Raw(att + "=\"" + (attValue ?? att) + "\"");
        }
        public static MvcHtmlString Att(this string attValue, string att)
        {
            return (!string.IsNullOrWhiteSpace(attValue)).Att(att, attValue);
        }

    }
}
