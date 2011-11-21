using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace FormFactory.Mvc
{
    /// <summary>
    /// This model binder uses implicit cast operators to try to bind action parameters.
    /// It wraps the default binder as it is a general purpose model binder
    /// </summary>
    public class ImplicitAssignmentBinder : IModelBinder
    {

        private readonly IModelBinder _defaultBinder;

        public ImplicitAssignmentBinder(IModelBinder defaultBinder)
        {
            _defaultBinder = defaultBinder;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //var defaultBinderResult = _defaultBinder.BindModel(controllerContext, bindingContext);
            //if (defaultBinderResult != null)
            //    return defaultBinderResult;
            var implicitAssignment =
                bindingContext.ModelType.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Static)
                    .Where(x => x.Name == "op_Implicit")
                    .Where(x => bindingContext.ModelType.IsAssignableFrom(x.ReturnType))
                .Where(x => x.GetParameters().Single().ParameterType == typeof(string))
                    .FirstOrDefault();

            if (implicitAssignment == null) //there is no implicit operator for binding
                return _defaultBinder.BindModel(controllerContext, bindingContext);

            var result = null as object;

            try
            {
                var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);
                var rawValue = (value == null ? null : value.RawValue as string[]) ?? new string[] { };
                result = implicitAssignment.Invoke(null, new object[] { rawValue.SingleOrDefault() });
            }
            catch (TargetInvocationException ex)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex.InnerException.Message);
                return null;
            }

            return result;
        }

    }
}
