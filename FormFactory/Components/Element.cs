using System;
using System.Collections.Generic;
using System.Text;

namespace FormFactory.Components
{
    public class Element
    {
        public string TagName { get; }

        public Element(string tagName)
        {
            TagName = tagName;
        }

        public Dictionary<string, string> Attributes { get; } = new Dictionary<string, string>();
        public string Value { get; set; }
    }
}
