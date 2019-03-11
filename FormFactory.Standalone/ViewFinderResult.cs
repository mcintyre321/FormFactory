namespace FormFactory.Standalone
{
    public class ViewFinderResult : IViewFinderResult
    {
        public ViewFinderResult(View view)
        {
            View = view;
        }

        public View View { get; }
    }
}