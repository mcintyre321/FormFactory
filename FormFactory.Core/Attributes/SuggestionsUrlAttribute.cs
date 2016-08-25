using System;

namespace FormFactory.Attributes
{
    public class SuggestionsUrlAttribute : Attribute
    {
        public string Url { get; private set; }

        public SuggestionsUrlAttribute(string url)
        {
            Url = url;
        }
    }
}