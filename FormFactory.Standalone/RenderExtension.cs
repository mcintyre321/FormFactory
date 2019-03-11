using System.Collections.Generic;
using System.Text;
using FormFactory.Standalone;

namespace FormFactory
{
    public static class RenderExtension
    {
        public static MvcHtmlString Render(this IEnumerable<PropertyVm> properties,MyFfHtmlHelper html)
        {
            var sb = new StringBuilder();
            foreach (var propertyVm in properties)
            {
                sb.AppendLine(propertyVm.Render(html).ToString());
            }
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString Render(this PropertyVm propertyVm,MyFfHtmlHelper html)
        {
            return (html.Partial("FormFactory/Form.Property", propertyVm));
        }

        public static MvcHtmlString Render(this FormVm formVm,MyFfHtmlHelper html)
        {
            return (html.Partial("FormFactory/Form", formVm));
        }
    }
}