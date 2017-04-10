using System;

namespace FormFactory.Attributes
{
    public class DisplayAttribute : Attribute
    {
        public DisplayAttribute()
        {
        }

        public string Name { get; set; }
        public string Prompt { get; set; }
    }
}