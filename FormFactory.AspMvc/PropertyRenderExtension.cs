using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using FormFactory.AspMvc.Wrappers;

namespace FormFactory
{
    public static class PropertyRenderExtension
    {
        public static IHtmlString Render<THelper>(this IEnumerable<PropertyVm<THelper>> properties) where THelper: FfHtmlHelper
        {
            return new MvcHtmlString(FormFactory.VmHelper.Render(properties));
        }

        public static IHtmlString Render<THelper>(this PropertyVm<THelper> propertyVm) where THelper: FfHtmlHelper
        {
            return new MvcHtmlString(FormFactory.VmHelper.Render(propertyVm));
        }
    }
}