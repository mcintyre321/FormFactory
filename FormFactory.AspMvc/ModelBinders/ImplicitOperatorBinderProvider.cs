using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace FormFactory.AspMvc.ModelBinders
{
    public class ImplicitOperatorBinderProvider : IModelBinderProvider
    {
        static readonly ConcurrentDictionary<Type, MethodInfo> cache = new ConcurrentDictionary<Type, MethodInfo>();
        public static MethodInfo GetImplicitConversionMethod(Type modelType)
        {

            return cache.GetOrAdd(modelType, UncachedGetImplicitConversionMethod);
        }

        private static MethodInfo UncachedGetImplicitConversionMethod(Type modelType)
        {
            return modelType.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static)
                .Where(x => x.Name == "op_Implicit")
                .Where(x => modelType.IsAssignableFrom(x.ReturnType))
                .Where(x => x.GetParameters().Single().ParameterType == typeof (string))
                .FirstOrDefault();
        }

        #region Implementation of IModelBinderProvider

        public IModelBinder GetBinder(Type modelType)
        {
            var implicitConversionMethod = GetImplicitConversionMethod(modelType);
            if (implicitConversionMethod == null) //there is no implicit operator for binding
                return null;

            return new ImplicitOperatorBinder(implicitConversionMethod);
        }
        class ImplicitOperatorBinder : IModelBinder
        {
            private readonly MethodInfo _implicitConversionMethod;

            public ImplicitOperatorBinder(MethodInfo implicitConversionMethod)
            {
                _implicitConversionMethod = implicitConversionMethod;
            }

            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                try
                {
                    var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                    bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);
                    var rawValue = (value == null ? null : value.RawValue as string[]) ?? new string[] { };
                    return _implicitConversionMethod.Invoke(null, new object[] { rawValue.SingleOrDefault() });
                }
                catch (TargetInvocationException ex)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex.InnerException);
                    return null;
                }
            }
        }
        #endregion
    }
}