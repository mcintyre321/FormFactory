using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace FormFactory.Mvc
{
    public class PolymorphicModelBinder : DefaultModelBinder
    {
        public static Func<Type, string> WriteTypeToString =
            t => MachineKey.Encode(Encoding.UTF7.GetBytes(t.AssemblyQualifiedName), MachineKeyProtection.All);
        public static Func<string, Type> ReadTypeFromString = 
            s => Type.GetType(Encoding.UTF7.GetString(MachineKey.Decode(s, MachineKeyProtection.All)));

        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            var typeSlug = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".__type");
            if (typeSlug != null)
            {
                var value = typeSlug.AttemptedValue as string;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var type = ReadTypeFromString(value);
                    if (type != null && modelType.IsAssignableFrom(type))
                    {
                        var instance = base.CreateModel(controllerContext, bindingContext, type);
                        bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(
                                                                                                         () => instance,
                                                                                                         type);
                        return instance;
                    }
                }
            }
            return base.CreateModel(controllerContext, bindingContext, modelType);

        }
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {
            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var typeSlug = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".__type");
            if (typeSlug != null && !string.IsNullOrWhiteSpace(typeSlug.AttemptedValue as string))
            {
                var concreteModelTypeName = Encoding.UTF7.GetString(MachineKey.Decode((string)typeSlug.AttemptedValue, MachineKeyProtection.All));
                var type = Type.GetType(concreteModelTypeName);
                if (type != null && bindingContext.ModelType.IsAssignableFrom(type))
                {
                    var instance = Activator.CreateInstance(type);
                    bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => instance, type);
                    base.BindModel(controllerContext, bindingContext);
                    return instance;
                }
            }
            return base.BindModel(controllerContext, bindingContext);
        }


        override protected ICustomTypeDescriptor GetTypeDescriptor(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.Model != null)
            {
                var concreteType = bindingContext.Model.GetType();

                if (Nullable.GetUnderlyingType(concreteType) == null)
                {
                    return new AssociatedMetadataTypeTypeDescriptionProvider(concreteType).GetTypeDescriptor(concreteType);
                }
            }

            return base.GetTypeDescriptor(controllerContext, bindingContext);
        }

    }
}