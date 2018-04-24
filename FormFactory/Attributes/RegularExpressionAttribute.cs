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
            var msg = !string.IsNullOrWhiteSpace(FriendlyFormat) ? ("be " + FriendlyFormat) : ($"match pattern '{Pattern}'");
            return $"{propertyVmDisplayName} must {msg}";
        }

        public string FriendlyFormat { get; set; }
        public string Pattern { get;  }
    }
}
