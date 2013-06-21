using System;
using System.Web.Mvc;
using FormFactory.ModelBinding;
using IValueProvider = FormFactory.ModelBinding.IValueProvider;

namespace FormFactory.AspMvc.ModelBinders
{
    public class FormFactoryModelBindingContext : IModelBindingContext
    {
        private readonly ModelBindingContext _bindingContext;

        public FormFactoryModelBindingContext(ModelBindingContext bindingContext)
        {
            _bindingContext = bindingContext;
        }

        public string ModelName
        {
            get { return _bindingContext.ModelName; }
        }

        public IValueProvider ValueProvider
        {
            get { return new FormFactoryValueProvider(_bindingContext.ValueProvider); } 
        }

        public object Model
        {
            get { return _bindingContext.Model; }
        }

        public void SetModelMetadataForType(Func<object> func, Type type)
        {
            _bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(func, type);
        }
    }
}