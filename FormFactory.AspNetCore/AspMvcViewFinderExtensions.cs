using System;
using System.Linq;

using FormFactory.AspMvc.Wrappers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FormFactory
{
    public static class AspMvcViewFinderExtensions
    {
        public static IHtmlContent BestProperty(this IHtmlHelper html, PropertyVm vm)
        {
            string viewName = ViewFinderExtensions.BestPropertyName(new FormFactoryHtmlHelper(html), vm);
            return html.Partial(viewName, vm);
        }

        public static string BestViewName(this IHtmlHelper html, Type type, string prefix = null)
        {
            return FormFactory.ViewFinderExtensions.BestViewName(new FormFactoryHtmlHelper(html), type, prefix);
        }
        public static IHtmlContent BestPartial(this IHtmlHelper html, object model, Type type = null, string prefix = null)
        {
            var partialViewName = BestViewName(html, type ?? model.GetType(), prefix);
            return html.Partial(partialViewName, model);
        }
        public static IHtmlContent Partial(this IHtmlHelper html, string partialViewName, object model, ViewDataDictionary vdd = null)
        {
            return html.PartialAsync(partialViewName, model, vdd).Result;
        }
    }
}
