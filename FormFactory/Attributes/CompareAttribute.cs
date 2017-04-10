using System;

namespace FormFactory.Attributes
{
    public class CompareAttribute : Attribute
    {
        private string propertyToCompareTo;
        public CompareAttribute(string propertyToCompareTo)
        {
            this.propertyToCompareTo = propertyToCompareTo;
        }

        public string ErrorMessage { get; set; }

        public string FormatErrorMessage(string fieldName)
        {
            return $"{fieldName} must be equal to {propertyToCompareTo}";
        }

    }
}