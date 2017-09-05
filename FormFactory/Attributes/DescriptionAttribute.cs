using System;

namespace FormFactory.Attributes
{
    public class DescriptionAttribute : Attribute
    {

        public DescriptionAttribute(string description)
        {
            this.Description = description;
        }

        public string Description { get; set; }
    }
}