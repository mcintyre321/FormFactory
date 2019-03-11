using System.Collections.Generic;
using System.Text;
using FormFactory.Standalone;

namespace FormFactory
{
    public static class RenderAsyncExtension
    {
        public static string Render(this IEnumerable<PropertyVm> properties)
        {
            var helper = new MyFfHtmlHelper();
            var sb = new StringBuilder();
            foreach (var propertyVm in properties)
            {
                sb.AppendLine(propertyVm.Render(helper).ToString()).ToString();
            }

            return sb.ToString();

        }


    }

    // the sample base template class. It's not mandatory but I think it's much easier.
}