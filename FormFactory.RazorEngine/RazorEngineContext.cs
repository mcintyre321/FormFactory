namespace FormFactory.RazorEngine
{
    public class RazorEngineContext : IViewFinder
    {
        private readonly RazorTemplateHtmlHelper _razorTemplateHtmlHelper;

        public RazorEngineContext(RazorTemplateHtmlHelper razorTemplateHtmlHelper)
        {
            _razorTemplateHtmlHelper = razorTemplateHtmlHelper;
        }

        public IViewFinderResult FindPartialView(string partialViewName)
        {
            var viewName = EmbeddedResourceRegistry.ResolveResourcePath(partialViewName);
            if (viewName == null) return new RazorTemplateViewFinderResult();
            return new RazorTemplateViewFinderResult()
                {
                    View = new RazorTemplateView()
                };
        }
    }
}