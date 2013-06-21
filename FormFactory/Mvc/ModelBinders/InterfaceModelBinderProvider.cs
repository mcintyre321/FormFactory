using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace FormFactory.Mvc.ModelBinders
{
    public class InterfaceModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            if (!modelType.IsInterface) return null; 
            if (IsInstanceOfGeneric(modelType, typeof(IEnumerable<>)))
            {
                modelType = typeof(List<>).MakeGenericType(modelType.GetGenericArguments().Single());
                return System.Web.Mvc.ModelBinders.Binders.GetBinder(modelType);

            }
            if (IsInstanceOfGeneric(modelType, typeof(ICollection<>)))
            {
                modelType = typeof(List<>).MakeGenericType(modelType.GetGenericArguments().Single());
                return System.Web.Mvc.ModelBinders.Binders.GetBinder(modelType);

            }
            return null;
        }

        public bool IsInstanceOfGeneric(Type type, Type generic)
        {
            return type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == generic);
        }
    }
}
