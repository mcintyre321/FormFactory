using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace FormFactory.Standalone
{
    public static class RenderAsyncExtension
    {
        public static async Task<string> RenderAsync(this IEnumerable<PropertyVm> properties)
        {
            var sb = new StringBuilder();
            foreach (var propertyVm in properties)
            {
                sb.AppendLine(await propertyVm.RenderAsync());
            }
            return sb.ToString();
        }

        public static async Task<string> RenderAsync(this FormVm formVm)
        {
            using (var serviceScope = RazorContextBuilder.ServiceScopeFactory.Value.CreateScope())
            {
                var helper = serviceScope.ServiceProvider.GetRequiredService<RazorViewToStringRenderer>();

                return await helper.RenderViewToStringAsync("Views/Shared/FormFactory/Form.cshtml", formVm);
            }
        }

        public static async Task<string> RenderAsync(this PropertyVm propertyVm)
        {
            using (var serviceScope = RazorContextBuilder.ServiceScopeFactory.Value.CreateScope())
            {
                var helper = serviceScope.ServiceProvider.GetRequiredService<RazorViewToStringRenderer>();

                return await helper.RenderViewToStringAsync("Views/Shared/FormFactory/Form.Property.cshtml",
                    propertyVm);
            }
        }
    }
}
