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
            return html.Raw(ViewFinderExtensions.BestProperty(new FormFactoryHtmlHelper(html), vm));
        }
        public static System.Web.IHtmlString BestViewName(this HtmlHelper helper, Type type, string prefix = null)
        {
            return helper.Raw(new FormFactoryHtmlHelper(helper).BestViewName(type, prefix));
        }
        public static System.Web.IHtmlString BestPartial(this HtmlHelper helper, object model, Type type = null, string prefix = null)
        {
            return helper.Raw(ViewFinderExtensions.BestPartial(new FormFactoryHtmlHelper(helper), model, type, prefix));
        }
    }
}
