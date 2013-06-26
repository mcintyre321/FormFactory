using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using FormFactory.AspMvc.Wrappers;

namespace FormFactory
{
    public static class AspMvcViewFinderExtensions
    {
        public static System.Web.IHtmlString BestProperty(this HtmlHelper html, PropertyVm vm)
        {
            return html.Partial(ViewFinderExtensions.BestPropertyName(new FormFactoryHtmlHelper(html), vm));
        }
        public static string BestViewName(this HtmlHelper html, Type type, string prefix = null)
        {
            return new FormFactoryHtmlHelper(html).BestViewName(type, prefix);
        }
        public static System.Web.IHtmlString BestPartial(this HtmlHelper html, object model, Type type = null, string prefix = null)
        {
            return html.Partial(ViewFinderExtensions.BestPartialName(new FormFactoryHtmlHelper(html), model));
            
        }
    }
}
