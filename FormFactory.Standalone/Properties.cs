using System;
using System.Collections.Generic;
using System.Linq;

namespace FormFactory.Standalone
{
    public class Properties
    {
        public static IEnumerable<PropertyVm> For(object target)
        {
            return FF.PropertiesFor(target, target.GetType());
        }
    }
}