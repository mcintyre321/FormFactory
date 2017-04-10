using System;

namespace FormFactory.Attributes
{
    public class RequiredAttribute : Attribute
    {
        public string FormatErrorMessage(string propertyVmDisplayName)
        {
            return $"{propertyVmDisplayName} is required";
        }
    }
}