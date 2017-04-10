//using System;
//using System.ComponentModel;
//using FormFactory.Attributes;
//using System.Web.Mvc;

//namespace FormFactory.AspMvc.ModelBinders
//{
//    public class PolymorphicModelBinder : DefaultModelBinder
//    {
//        private ModelBinding.PolymorphicModelBinder _ffBinder;

//        public PolymorphicModelBinder()
//        {
//            _ffBinder = new ModelBinding.PolymorphicModelBinder();
//        }
         


//        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
//        {
//            return _ffBinder.CreateModel(
//                new FormFactoryModelBindingContext(bindingContext),
//                modelType, 
//                () => base.CreateModel(controllerContext, bindingContext, modelType), s => new Encoder().ReadTypeFromString(s));
//        }

//        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
//        {
//            return _ffBinder.BindModel(
              
//              new FormFactoryModelBindingContext(bindingContext),
//              (t, bc) =>
//              {
//                  var o = Activator.CreateInstance(t);
//                  bc.SetModelMetadataForType(() => o, t);
//                  base.BindModel(controllerContext, bindingContext);
//                  return o;
//              },
//              () => base.BindModel(controllerContext, bindingContext), s => new Encoder().ReadTypeFromString(s));
//        }
         

//        override protected ICustomTypeDescriptor GetTypeDescriptor(ControllerContext controllerContext, ModelBindingContext bindingContext)
//        {
//            if (bindingContext.Model != null)
//            {
//                var concreteType = bindingContext.Model.GetType();

//                if (Nullable.GetUnderlyingType(concreteType) == null)
//                {
//                    return new AssociatedMetadataTypeTypeDescriptionProvider(concreteType).GetTypeDescriptor(concreteType);
//                }
//            }

//            return base.GetTypeDescriptor(controllerContext, bindingContext);
//        }

//    }
//}