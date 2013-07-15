using System.Collections.Generic;
using System.Text;
using System.Web;
using RazorEngine.Text;

namespace FormFactory.RazorEngine
{
    public static class PropertyRenderExtension
    {
        public static RawString Render(this IEnumerable<PropertyVm> properties, RazorTemplateHtmlHelper html)
        {
            var sb = new StringBuilder();
            foreach (var propertyVm in properties)
            {
                sb.AppendLine(propertyVm.Render(html).ToString());
            }
            return new RawString(sb.ToString());
        }

        public static RawString Render(this PropertyVm propertyVm, RazorTemplateHtmlHelper html)
        {
            return (html.Partial("FormFactory/Form.Property", propertyVm));
        }
    }
}