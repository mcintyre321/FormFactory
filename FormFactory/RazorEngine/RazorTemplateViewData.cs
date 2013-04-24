namespace FormFactory.RazorEngine
{
    public class RazorTemplateViewData : ViewData
    {
        private readonly RazorTemplateHtmlHelper _razorTemplateHtmlHelper;

        public RazorTemplateViewData(RazorTemplateHtmlHelper razorTemplateHtmlHelper)
        {
            _razorTemplateHtmlHelper = razorTemplateHtmlHelper;
        }

        public IModelStateDictionary ModelState { get { return new RazorTemplateModelStateDictionary(); } }
        public object Model { get { return _razorTemplateHtmlHelper.Model; } }
    }
}