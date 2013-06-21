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
            return new FormFactoryValueProviderResult(_valueProvider.GetValue(key));

        }
    }
}