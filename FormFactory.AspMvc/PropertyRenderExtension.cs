using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using FormFactory.AspMvc.Wrappers;

namespace FormFactory.AspMvc
{
    public static class PropertyRenderExtension
    {
        public static IHtmlString Render(this IEnumerable<PropertyVm> properties, HtmlHelper html)
        {
            var sb = new StringBuilder();
            foreach (var propertyVm in properties)
            {
                sb.AppendLine(propertyVm.Render(html).ToString());
            }
            return new MvcHtmlString(sb.ToString());
        }

        public static IHtmlString Render(this PropertyVm propertyVm, HtmlHelper html)
        {
            return (html.Partial("FormFactory/Form.Property", propertyVm));
        }
    }
     
}