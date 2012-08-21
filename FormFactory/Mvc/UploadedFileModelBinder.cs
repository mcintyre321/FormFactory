using System;
using System.Web;
using System.Web.Mvc;
using FormFactory.ValueTypes;

namespace FormFactory.Mvc
{
    internal class UploadedFileModelBinder<TUploadedFile> : IModelBinder where TUploadedFile : UploadedFile, new()
    {
        private readonly Func<HttpPostedFileBase, ControllerContext, ModelBindingContext, TUploadedFile> _doSave;

        internal UploadedFileModelBinder(Func<HttpPostedFileBase, ControllerContext, ModelBindingContext, TUploadedFile> doSave)
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
            if (request.Params[bindingContext.ModelName + ".Id"] != null)
            {
                // TODO: better model binding? bindingContext.ValueProvider.GetValue(bindingContext.ModelName) returns null..
                int contentLength;
                int.TryParse(request.Params[bindingContext.ModelName + ".ContentLength"], out contentLength);
                return new TUploadedFile
                           {
                               Id = request.Params[bindingContext.ModelName + ".Id"],
                               ContentLength = contentLength,
                               ContentType = request.Params[bindingContext.ModelName + ".ContentType"],
                               FileName = request.Params[bindingContext.ModelName + ".FileName"],
                               Uri = request.Params[bindingContext.ModelName + ".Uri"],
                           };
            }
            return null;
        }
    }
}