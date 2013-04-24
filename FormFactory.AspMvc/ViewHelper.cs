using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FormFactory.AspMvc
{
    public static class ViewHelper
    {
        public static System.Web.IHtmlString Raw(this bool value, string output)
        {
            return new System.Web.HtmlString(value ? output : "");
        }
        public static System.Web.IHtmlString InputAtts(this PropertyVm vm)
        {
            return new System.Web.HtmlString(string.Join(" ", new string[]{vm.Disabled().ToHtmlString(), vm.Readonly().ToHtmlString(), vm.DataAtts().ToHtmlString()}));
        }
        public static System.Web.IHtmlString Disabled(this PropertyVm vm)
        {
            return vm.Disabled.Att("disabled");
        }
        public static System.Web.IHtmlString Readonly(this PropertyVm vm)
        {
            return vm.Readonly.Att("readonly");
        }
        public static System.Web.IHtmlString DataAtts(this PropertyVm vm)
        {
            var sb = new StringBuilder();
            foreach (var att in vm.DataAttributes)
            {
                sb.Append("data-" + att.Key + "='" + att.Value + "' ");
            }
            return new System.Web.HtmlString(sb.ToString());
        }

        public static System.Web.IHtmlString Att(this bool value, string att, string attValue = null)
        {
            return value.Raw(att + "=\"" + (attValue ?? att) + "\"");
        }
        public static System.Web.IHtmlString Att(this string attValue, string att)
        {
            return (!string.IsNullOrWhiteSpace(attValue)).Att(att, attValue);
        }

    }

    
   
}
