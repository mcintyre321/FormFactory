using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
