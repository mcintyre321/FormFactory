using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FormFactory.AspMvc.Wrappers;

namespace FormFactory
{
    public static class AspMvcViewFinderExtensions
    {
        public static System.Web.IHtmlString BestProperty(this HtmlHelper html, PropertyVm vm)
        {
            return html.Raw(new FormFactoryHtmlHelper(html).BestProperty(vm));
        }
        public static System.Web.IHtmlString BestViewName(this HtmlHelper helper, Type type, string prefix = null)
        {
            return helper.Raw(new FormFactoryHtmlHelper(helper).BestViewName(type, prefix));
        }
        public static System.Web.IHtmlString BestPartial(this HtmlHelper helper, object model, Type type = null, string prefix = null)
        {
            return helper.Raw(new FormFactoryHtmlHelper(helper).BestPartial(model, type, prefix));
        }
    }
}
