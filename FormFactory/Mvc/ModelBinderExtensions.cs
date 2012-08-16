using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using FormFactory.ValueTypes;

namespace FormFactory.Mvc
{
    public static class ModelBinderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBinders"></param>
        /// <param name="doUpload">Function or method to take an HttpPostedFileBase, store it somewhere, and return the stored location's url.
        /// Optional: leave blank to store in default DataDirectory.</param>
        public static void RegisterUploadedFileModelBinder(this ModelBinderDictionary modelBinders, Func<HttpPostedFileBase, ControllerContext, ModelBindingContext, string> doUpload = null)
        {
            modelBinders.Add(typeof(UploadedFile), new UploadedFileModelBinder<UploadedFile>(doUpload));
        }
    }
}
