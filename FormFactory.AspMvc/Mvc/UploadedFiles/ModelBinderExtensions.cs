using System.Web.Mvc;
using FormFactory.ValueTypes;

namespace FormFactory.AspMvc.Mvc.UploadedFiles
{
    public static class ModelBinderExtensions
    {
        public static void RegisterUploadedFileModelBinder(this ModelBinderDictionary modelBinders)
        {
            modelBinders.Add(typeof(UploadedFile), new UploadedFileModelBinder<UploadedFile>( FileStores.AppData));
        }
    }
}
