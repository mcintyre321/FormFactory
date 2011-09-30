using System.Linq;
using System.Web.Mvc;

namespace FormFactory
{
    public static class AllValidationMessagesHelper
    {

        public static MvcHtmlString AllValidationMessages(this HtmlHelper helper, string modelName)
        {
            if (HasErrors(helper, modelName))
            {
                var message = string.Join(", ", helper.ViewData.ModelState[modelName].Errors.Select(e => e.ErrorMessage));
                return new MvcHtmlString(message);
            }
            return new MvcHtmlString("");
        }

        public static bool HasErrors(this HtmlHelper helper, string modelName)
        {
            return helper.ViewData.ModelState.ContainsKey(modelName) &&
                   helper.ViewData.ModelState[modelName].Errors.Count > 0;
        }
    }
}