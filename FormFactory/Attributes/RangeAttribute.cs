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
            return string.Format(Resources.Range, fieldName, Minimum, Maximum);
        }

        public object Minimum { get;  }
        public object Maximum { get;  }
    }
}