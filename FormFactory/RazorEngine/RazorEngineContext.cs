namespace FormFactory.RazorEngine
{
    public class RazorEngineContext : FfContext
    {
        private readonly RazorTemplateHtmlHelper _razorTemplateHtmlHelper;

        public RazorEngineContext(RazorTemplateHtmlHelper razorTemplateHtmlHelper)
        {
            _razorTemplateHtmlHelper = razorTemplateHtmlHelper;
        }

        public ViewResult FindPartialView(string partialViewName)
        {
            var viewName = EmbeddedResourceRegistry.ResolveResourcePath(partialViewName);
            if (viewName == null) return new RazorTemplateViewResult();
            return new RazorTemplateViewResult()
                {
                    View = new RazorTemplateView()
                };
        }
    }
}