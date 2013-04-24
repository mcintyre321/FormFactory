using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using FormFactory.Attributes;
using RazorEngine.Text;

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
            return new HtmlString(string.Join(" ", new string[] { vm.Disabled().ToEncodedString(), vm.Readonly().ToEncodedString(), vm.DataAtts().ToEncodedString() }));
        }
        public static IHtmlString Disabled(this PropertyVm vm)
        {
            return Attr(vm.Disabled, "disabled", null);
        }
        public static IHtmlString Readonly(this PropertyVm vm)
        {
            return Attr(vm.Readonly, "readonly", null);
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

        public static IHtmlString Attr(bool value, string att, string attValue = null)
        {
            return value.Raw(att + "=\"" + (attValue ?? att) + "\"");
        }

        public static IHtmlString Placeholder(PropertyVm pi)
        {
            var placeHolderText = pi.GetCustomAttributes().OfType<PlaceholderAttribute>().Select(a => a.Text).FirstOrDefault();
            return Attr((!string.IsNullOrWhiteSpace(placeHolderText)), "placeholder", placeHolderText);
        }
    }

    
    public interface IHtmlString : IEncodedString
    {
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
       
        public override string ToString()
        {
            return _value;
        }

        public string ToEncodedString()
        {
            return _value;
            
        }
    }
}
