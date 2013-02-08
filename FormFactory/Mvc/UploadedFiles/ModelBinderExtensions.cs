using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using FormFactory.ValueTypes;

namespace FormFactory.Mvc.UploadedFiles
{
    public static class ModelBinderExtensions
    {
        public static void RegisterUploadedFileModelBinder(this ModelBinderDictionary modelBinders)
        {
            modelBinders.Add(typeof(UploadedFile), new UploadedFileModelBinder<UploadedFile>( FileStores.AppData));
        }
    }
}
