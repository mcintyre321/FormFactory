using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormFactory.AspMvc.Example.Models.Examples
{
    public class InhertitanceExample : BaseClass
    {
        public string InheritingClassPropery { get; set; }
    }

    public class BaseClass
    {
        public string BaseClassProperty { get; set; }
    }
}