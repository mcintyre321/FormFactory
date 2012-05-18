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
            return vm.Disabled.Att("disabled");
        }
        public static MvcHtmlString Readonly(this PropertyVm vm)
        {
            return vm.Readonly.Att("readonly");
        }
        public static MvcHtmlString Att(this bool value, string att)
        {
            return new MvcHtmlString(value ? att + "\"" + att + "\"" : "");
        }

    }
}
