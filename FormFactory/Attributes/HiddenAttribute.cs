using System.ComponentModel.DataAnnotations;

namespace FormFactory.Attributes
{
    public class HiddenAttribute : DataTypeAttribute
    {
        public HiddenAttribute() : base("Hidden")
        {
        }

        public HiddenAttribute(string customDataType) : base(customDataType)
        {
        }
    }
}
