using System.Web.Mvc;

namespace FormFactory.AspMvc.Wrappers
{
    public class FormFactoryContext : FfContext
    {
        private readonly ControllerContext _cc;

        public FormFactoryContext(ControllerContext cc)
        {
            _cc = cc;
        }

        public ViewResult FindPartialView(string partialViewName)
        {
            var viewEngineResult = ViewEngines.Engines.FindPartialView(_cc, partialViewName);
            if (viewEngineResult == null) return null;
            return new FormFactoryViewResult(viewEngineResult);
        }
    }
}