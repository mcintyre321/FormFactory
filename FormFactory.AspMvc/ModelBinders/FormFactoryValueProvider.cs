using FormFactory.ModelBinding;

namespace FormFactory.AspMvc.ModelBinders
{
    public class FormFactoryValueProvider : IValueProvider
    {
        private readonly System.Web.Mvc.IValueProvider _valueProvider;

        public FormFactoryValueProvider(System.Web.Mvc.IValueProvider valueProvider)
        {
            _valueProvider = valueProvider;
        }

        public IValueProviderResult GetValue(string key)
        {
            var valueProviderResult = _valueProvider.GetValue(key);
            if (valueProviderResult == null) return null;
            return new FormFactoryValueProviderResult(valueProviderResult);
        }
    }
}