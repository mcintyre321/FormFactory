using System;

namespace FormFactory.Attributes
{
    public class RegularExpressionAttribute : Attribute {
        public RegularExpressionAttribute(string pattern)
        {
            Pattern = pattern;
        }

        public string FormatErrorMessage(string propertyVmDisplayName)
        {
            return $"{propertyVmDisplayName} must be ${FriendlyFormat ?? $"match pattern '{Pattern}'"}";
        }

        public string FriendlyFormat { get; set; }
        public string Pattern { get;  }
    }
}