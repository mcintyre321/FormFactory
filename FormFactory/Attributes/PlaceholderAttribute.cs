using System;

namespace FormFactory.Attributes
{
    public class PlaceholderAttribute : Attribute
    {
        private readonly string _text;
        public string Text { get { return _text; } }

        public PlaceholderAttribute(string text)
        {
            this._text = text;
        }
    }
}