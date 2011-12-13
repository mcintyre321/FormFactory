using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using RazorGenerator.Mvc;

[assembly: WebActivator.PostApplicationStartMethod(typeof(FormFactory.App_Start.FormFactoryRazorGeneratorMvcStart), "Start")]

namespace FormFactory.App_Start {
    public static class FormFactoryRazorGeneratorMvcStart {
        public static void Start() {
            var engine = new PrecompiledMvcEngine(typeof(FormFactoryRazorGeneratorMvcStart).Assembly)
            {
                UsePhysicalViewsIfNewer = true
            };

            ViewEngines.Engines.Add(engine);

            //// StartPage lookups are done by WebPages. 
            //VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
        }
    }
}
