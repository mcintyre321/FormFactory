using System.Collections.Generic;
using System.Web;
using RazorEngine.Text;

namespace FormFactory.RazorEngine
{
    public static class PropertyRenderExtension
    {
        public static RawString Render(this IEnumerable<PropertyVm<RazorTemplateHtmlHelper>> properties)
        {
            return new RawString(FormFactory.VmHelper.Render(properties));
        }

        public static RawString Render(this PropertyVm<RazorTemplateHtmlHelper> propertyVm)
        {
            return new RawString(FormFactory.VmHelper.Render(propertyVm));
        }
    }
}