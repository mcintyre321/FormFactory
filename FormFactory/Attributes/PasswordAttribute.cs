using System;

namespace FormFactory.Attributes
{
    public class PasswordAttribute : Attribute
    {
        public string DataFormatString { get; set; }
    }
}