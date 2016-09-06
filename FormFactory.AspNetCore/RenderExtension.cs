using System.Collections.Generic;
using System.Text;
using FormFactory.AspMvc.Wrappers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FormFactory.AspMvc
{
    public static class RenderExtension
    {
        public static IHtmlContent Render(this IEnumerable<PropertyVm> properties, IHtmlHelper html)
        {
            var sb = new StringBuilder();
            foreach (var propertyVm in properties)
            {
                sb.AppendLine(propertyVm.Render(html).ToHtmlString());
            }
            return new HtmlString(sb.ToString());
        }

        public static IHtmlContent Render(this PropertyVm propertyVm, IHtmlHelper html)
        {
            return (html.Partial("FormFactory/Form.Property", propertyVm));
        }

        public static IHtmlContent Render(this FormVm formVm, IHtmlHelper html)
        {
            return (html.Partial("FormFactory/Form", formVm));
        }
    }
}