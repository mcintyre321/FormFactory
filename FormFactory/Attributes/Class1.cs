using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormFactory.Attributes
{
    public abstract class FormFactoryPropertyAttribute : Attribute
    {
        public abstract void ApplyToProperty(PropertyVm vm);
    }
}
