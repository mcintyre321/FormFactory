using System;

namespace FormFactory.Attributes
{
    public class RangeAttribute : Attribute
    {
        private Func<object, bool> isValid;
        public RangeAttribute(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
            isValid = (value) => ((int) value) >= minimum && ((int) value <= maximum);
        }

        public string FormatErrorMessage(string fieldName)
        {
            return $"{fieldName} must be between {Minimum} and {Maximum}";
        }

        public object Minimum { get;  }
        public object Maximum { get;  }
    }
}