using System;
using System.Web;
using System.Web.Mvc;
using FormFactory.ValueTypes;

namespace FormFactory.Mvc
{
    internal class UploadedFileModelBinder : IModelBinder
    {
        private readonly Func<HttpPostedFileBase, string> _doUpload;

        internal UploadedFileModelBinder(Func<HttpPostedFileBase, string> doUpload)
        {
            if (doUpload == null)
            {
                throw new ArgumentNullException("doUpload");
            }
            _doUpload = doUpload;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            var file = request.Files[bindingContext.ModelName];
            if (file != null && file.ContentLength != 0)
            {
                var url = _doUpload(file);
                return new UploadedFile { Url = url };
            }
            return null;
        }
    }
}