

using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Routing;

namespace FormFactory.AspMvc.Wrappers
{
    public class FormFactoryContext : IViewFinder
    {
        private readonly ViewContext _vc;

        public FormFactoryContext(ViewContext vc)
        {
            _vc = vc;
        }

        public IViewFinderResult FindPartialView(string partialViewName)
        {
            var service = (ICompositeViewEngine) _vc.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine));
            var ac = new ActionContext(_vc.HttpContext, _vc.RouteData, _vc.ActionDescriptor);
            var viewEngineResult = service.FindView(ac, partialViewName, false);
            if (viewEngineResult == null) return null;
            return new FormFactoryViewFinderResult(viewEngineResult);
        }
 
    }
}