namespace FormFactory.Standalone
{
    public class RazorLightContext : IViewFinder
    {
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