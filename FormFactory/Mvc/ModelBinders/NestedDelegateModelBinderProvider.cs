using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Web.Mvc;

namespace FormFactory.Mvc.ModelBinders
{
    public delegate object ModelBinderDelegate(ControllerContext cc, ModelBindingContext mbc);
    public class NestedDelegateModelBinderProvider : IModelBinderProvider
    {
        private ConcurrentDictionary<Type, IModelBinder> binders = new ConcurrentDictionary<Type, IModelBinder>();
        public IModelBinder GetBinder(Type modelType)
        {
            return binders.GetOrAdd(modelType, t =>
            {
                var binderFunc = t.GetField("ModelBinder", BindingFlags.Static | BindingFlags.Public);
                
                if (binderFunc != null)
                {
                    var mbd = (ModelBinderDelegate) binderFunc.GetValue(null);
                    return new DelegateModelBinder(mbd);
                }
                return null;
            });

        }

        class DelegateModelBinder : IModelBinder
        {
            private ModelBinderDelegate mbd;

            public DelegateModelBinder(ModelBinderDelegate mbd)
            {
                this.mbd = mbd;
            }

            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                return mbd(controllerContext, bindingContext);
            }
        }
    }
}