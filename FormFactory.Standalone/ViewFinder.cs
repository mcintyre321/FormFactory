namespace FormFactory.Standalone
{
    public class ViewFinder : IViewFinder
    {
        public IViewFinderResult FindPartialView(string partialViewName)
        {
            var name = "FormFactory.Views.Shared." + partialViewName.Replace('/', '.') + ".cshtml";
            var stream = typeof(FF).Assembly.GetManifestResourceStream(name);
            return new ViewFinderResult(stream == null ? null : new MyView());
        }
    }
}