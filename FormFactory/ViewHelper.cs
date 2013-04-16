using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FormFactory
{
    public static class ViewHelper
    {
        public static IHtmlString Raw(this bool value, string output)
        {
            return new HtmlString(value ? output : "");
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
            return new HtmlString(sb.ToString());
        }

        public static IHtmlString Att(this bool value, string att, string attValue = null)
        {
            return value.Raw(att + "=\"" + (attValue ?? att) + "\"");
        }
        public static IHtmlString Att(this string attValue, string att)
        {
            return (!string.IsNullOrWhiteSpace(attValue)).Att(att, attValue);
        }

    }

    
    public interface IHtmlString
    {
        string ToHtmlString();
    }

    public class HtmlString : IHtmlString
    {
        public HtmlString(string value)
        {
            _value = value;
        }

        public static implicit operator HtmlString(string value)
        {
            return new HtmlString(value);
        }

        private string _value;
        public string ToHtmlString()
        {
            return _value;
        }
        public override string ToString()
        {
            return _value;
        }
    }
}
