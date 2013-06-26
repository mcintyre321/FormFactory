using System.Collections.Generic;
using System.Text;
using System.Web;
using RazorEngine.Text;

namespace FormFactory.RazorEngine
{
    public static class PropertyRenderExtension
    {
        public static RawString Render(this IEnumerable<PropertyVm<RazorTemplateHtmlHelper>> properties)
        {
            var sb = new StringBuilder();
            foreach (var propertyVm in properties)
            {
                sb.AppendLine(propertyVm.Render().ToString());
            }
            return new RawString(sb.ToString());
        }

        public static RawString Render(this PropertyVm<RazorTemplateHtmlHelper> propertyVm)
        {
            return (propertyVm.Html.Partial("FormFactory/Form.Property", propertyVm));
        }
    }
}