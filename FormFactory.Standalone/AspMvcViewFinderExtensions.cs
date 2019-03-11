using System;
using System.Threading.Tasks;
using FormFactory.Standalone;

namespace FormFactory
{
    public static class AspMvcViewFinderExtensions
    {
        public static MvcHtmlString BestProperty(this MyFfHtmlHelper html, PropertyVm vm)
        {
            string viewName = ViewFinderExtensions.BestPropertyName(html, vm);
            return html.Partial(viewName, vm);
        }

        public static string BestViewName(this MyFfHtmlHelper html, Type type, string prefix = null)
        {
            return FormFactory.ViewFinderExtensions.BestViewName(html, type, prefix);
        }
        public static MvcHtmlString BestPartial(this MyFfHtmlHelper html, object model, Type type = null, string prefix = null)
        {
            var partialViewName = BestViewName(html, type ?? model.GetType(), prefix);
            return html.Partial(partialViewName, model);
        }
    }
}
