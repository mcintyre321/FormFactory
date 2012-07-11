using System;
using System.Web;
using System.Web.Mvc;
using FormFactory.ValueTypes;

namespace FormFactory.Mvc
{
    internal class UploadedFileModelBinder : IModelBinder
    {
        private readonly Func<HttpPostedFileBase, ControllerContext, ModelBindingContext, string> _doUpload;

        internal UploadedFileModelBinder(Func<HttpPostedFileBase, ControllerContext, ModelBindingContext, string> doUpload)
        {
            _doUpload = doUpload ?? ((f, c, m) => AppDataFileUploader.Upload(f));
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            var file = request.Files[bindingContext.ModelName];
            if (file != null && file.ContentLength != 0)
            {
                var url = _doUpload(file, controllerContext, bindingContext);
                return new UploadedFile { Url = url };
            }
            return null;
        }
    }
}