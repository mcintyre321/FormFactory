using System;

namespace FormFactory.Attributes
{
    public class DisplayFormatAttribute : Attribute
    {
        public string DataFormatString { get; set; }
    }
}