using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FormFactory
{
    public static class ViewHelper
    {
        public static HtmlString Disabled(this PropertyVm vm)
        {
            return new MvcHtmlString(vm.Disabled ? "disabled=\"disabled\"" : "");
        }
        public static MvcHtmlString Readonly(this PropertyVm vm)
        {
            return new MvcHtmlString(vm.Readonly ? "readonly=\"readonly\"" : "");
        }

    }
}
