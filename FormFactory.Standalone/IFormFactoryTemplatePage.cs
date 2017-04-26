namespace FormFactory.Standalone
{
    public interface IFormFactoryTemplatePage
    {
        RazorTemplateHtmlHelper Html { get; set; }
        ViewData ViewData { get; set; }
    }
}