using System;

namespace FormFactory.Attributes
{
    public class StringLengthAttribute : Attribute
    {
        private Func<string, bool> isValid;
        public StringLengthAttribute(int maximumLength)
        {
            MaximumLength = maximumLength;
            isValid = (value) => (value.Length) >= MinimumLength && ((int)value.Length <= MaximumLength);
        }

        public string FormatErrorMessage(string fieldName)
        {
            return string.Format(Resources.StringLength, fieldName, MinimumLength, MaximumLength);
        }

        public int MinimumLength { get; set; } = 0;
        public int MaximumLength { get; }
    }
}