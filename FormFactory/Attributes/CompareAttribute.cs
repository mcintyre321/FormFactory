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
            return string.Format(Resources.Compare, fieldName, propertyToCompareTo);
        }

    }
}