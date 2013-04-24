using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FormFactory
{
    
    
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
    }
}
