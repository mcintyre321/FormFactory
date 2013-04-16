using System;
using System.Web;
using System.Web.Mvc;
using FormFactory.ValueTypes;

namespace FormFactory.AspMvc.Mvc.UploadedFiles
{
    public class UploadedFileModelBinder<TUploadedFile> : IModelBinder where TUploadedFile : UploadedFile, new()
    {
        private readonly Func<HttpPostedFileBase, ControllerContext, ModelBindingContext, TUploadedFile> _doSave;

        public UploadedFileModelBinder(Func<HttpPostedFileBase, ControllerContext, ModelBindingContext, TUploadedFile> doSave)
        {
            _doSave = doSave ?? ((f, c, m) => SimpleAppDataFileUploader.DoSave<TUploadedFile>(m.ModelState.IsValid, f));
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            var file = request.Files[bindingContext.ModelName];
            if (file != null && file.ContentLength != 0)
            {
                return _doSave(file, controllerContext, bindingContext);
            }
            var model = System.Web.Mvc.ModelBinders.Binders.DefaultBinder.BindModel(controllerContext, bindingContext) as TUploadedFile;
            if (model != null)
            {
                return model;
            }
            return null;
        }
    }
}
 