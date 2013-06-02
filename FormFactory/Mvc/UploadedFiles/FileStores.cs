using System.Web;
using System.Web.Mvc;
using FormFactory.ValueTypes;

namespace FormFactory.Mvc.UploadedFiles
{
    public class FileStores
    {
        public static IFileStore AppDataFileStore{get{return new AppDataFileStore();  }}
    }

    public interface IFileStore
    {
        UploadedFile Store(HttpPostedFileBase httpPostedFileBase, ControllerContext controllerContext, ModelBindingContext modelBindingContext);
        UploadedFile GetById(string id);
    }
}