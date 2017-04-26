using System.Threading.Tasks;
using RazorLight;

namespace FormFactory.Standalone
{
    public class FormFactoryTemplatePage<T> : RazorLight.TemplatePage<T>, IFormFactoryTemplatePage
    {
        public RazorTemplateHtmlHelper Html { get; set; }
        public ViewData ViewData { get; set; }



        public FormFactoryTemplatePage()
        {
            ViewData = new ViewData();
        }



        public override Task ExecuteAsync()
        {
            return Task.FromResult(0);
        }
    }
}