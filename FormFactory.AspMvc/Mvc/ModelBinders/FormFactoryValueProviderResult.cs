using System.Web.Mvc;
using FormFactory.ModelBinding;

namespace FormFactory.AspMvc.Mvc.ModelBinders
{
    public class FormFactoryValueProviderResult : IValueProviderResult
    {
        private readonly ValueProviderResult _getValue;

        public FormFactoryValueProviderResult(ValueProviderResult getValue)
        {
            _getValue = getValue;
        }

        public string AttemptedValue
        {
            get { return _getValue.AttemptedValue; }
        
        }
    }
}