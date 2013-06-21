using System;
using System.Collections.Concurrent;
using System.Web.Mvc;

namespace FormFactory.AspMvc.ModelBinders
{
    public class NestedModelBinderProvider : IModelBinderProvider
    {
         
        private ConcurrentDictionary<Type, IModelBinder> binders = new ConcurrentDictionary<Type, IModelBinder>();
        public IModelBinder GetBinder(Type modelType)
        {
            return binders.GetOrAdd(modelType, t =>
            {
                var nestedType = t.GetNestedType(t.Name + "Binder");
                if (nestedType != null)
                {
                    return (IModelBinder) Activator.CreateInstance(nestedType);
                }
                return null;
            });

        }

    }
}