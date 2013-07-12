using System.Web.Mvc;

namespace FormFactory.AspMvc.Wrappers
{
    public class FormFactoryViewFinderResult : IViewFinderResult
    {
        private readonly ViewEngineResult _findPartialView;

        public FormFactoryViewFinderResult(ViewEngineResult findPartialView)
        {
            _findPartialView = findPartialView;
        }

        public View View { get
        {
            if (_findPartialView.View == null) return null;
            return new FormFactoryView(_findPartialView.View);
        } }
    }
}