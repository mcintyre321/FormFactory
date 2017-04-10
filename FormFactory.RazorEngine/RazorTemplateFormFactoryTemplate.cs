using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using RazorLight.Templating;

namespace FormFactory.RazorEngine
{
    public interface IRazorTemplateFormFactoryTemplate
    {
        RazorTemplateHtmlHelper Html { get; set; }
        ViewData ViewData { get; set; }
        void SetModel(object o);
        string Run(ExecuteContext executeContext);
    }

    public class RazorTemplateFormFactoryTemplate<T> : TemplateBase<T>, IRazorTemplateFormFactoryTemplate
    {
        public RazorTemplateHtmlHelper Html { get; set; }
        public ViewData ViewData { get; set; }


        public void SetModel(object o)
        {
            this.Model = (T) o;
        }

        public string Run(ExecuteContext executeContext)
        {
            var writer = new StringWriter();
            ((ITemplate) this).Run(executeContext, writer);
            return writer.ToString();
        }

        public RazorTemplateFormFactoryTemplate()
        {
            ViewData = new ViewData();
        }
    }

    public class DictionaryDynamic : DynamicObject
    {
        private readonly IDictionary<string, object> _viewData;

        public DictionaryDynamic(IDictionary<string, object> viewData)
        {
            _viewData = viewData;
        }
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _viewData.Keys;
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (_viewData.ContainsKey(binder.Name))
            {
                result = _viewData[binder.Name];
                return true;
            }
            result = null;
            return true;
        }
    }

}