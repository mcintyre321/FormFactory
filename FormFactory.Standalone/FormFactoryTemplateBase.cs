using System.Threading.Tasks;
using RazorLight;

namespace FormFactory.Standalone
{
    public class FormFactoryTemplateBase : TemplatePage
    {
        public RazorTemplateHtmlHelper Html { get; set; }
        public ViewData ViewData { get; set; }



        public FormFactoryTemplateBase()
        {
            ViewData = new ViewData();
        }



        public override Task ExecuteAsync()
        {
            return Task.FromResult(0);
        }
    }
}