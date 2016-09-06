using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace FormFactory.AspMvc.Wrappers
{
    public static class ToHtmlStringExtension
    {
        public static string ToHtmlString(this IHtmlContent content)
        {
            var writer = new System.IO.StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}