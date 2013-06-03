using System.Web.Mvc;

namespace FormFactory.AspMvc.UploadedFiles
{
    public static class ModelBinderExtensions
    {
        public static void RegisterUploadedFileModelBinder(this ModelBinderDictionary modelBinders)
        {
            modelBinders.Add(typeof(UploadedFile), new UploadedFileModelBinder<UploadedFile>( FileStores.AppDataFileStore.Store));
        }
    }
}
