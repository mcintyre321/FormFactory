using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using FormFactory.AspMvc.Wrappers;

namespace FormFactory
{
    public static class AspMvcViewFinderExtensions
    {
        public static System.Web.IHtmlString BestProperty(this HtmlHelper html, PropertyVm vm)
        {
            string viewName = ViewFinderExtensions.BestPropertyName(new FormFactoryHtmlHelper(html), vm);
            return html.Partial(viewName, vm);
        }

        public static string BestViewName(this HtmlHelper html, Type type, string prefix = null)
        {
            return FormFactory.ViewFinderExtensions.BestViewName(new FormFactoryHtmlHelper(html), type, prefix);
        }
        public static System.Web.IHtmlString BestPartial(this HtmlHelper html, object model, Type type = null, string prefix = null)
        {
            var partialViewName = BestViewName(html, type ?? model.GetType(), prefix);
            return html.Partial(partialViewName, model);
        }
    }
}
