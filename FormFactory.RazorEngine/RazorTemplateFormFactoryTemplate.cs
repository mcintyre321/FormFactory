using System.Collections.Generic;
using System.Dynamic;
using RazorEngine.Templating;

namespace FormFactory.RazorEngine
{ 
    public class RazorTemplateFormFactoryTemplate<T> : TemplateBase<T> 
    {
        public RazorTemplateHtmlHelper Html { get; set; }
        public IDictionary<string, object> ViewData { get; set; }
        public RazorTemplateFormFactoryTemplate()
        {
            ViewData = new Dictionary<string, object>();
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