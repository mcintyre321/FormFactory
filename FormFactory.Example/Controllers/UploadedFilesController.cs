using System.Web.Mvc;
using FormFactory.AspMvc.UploadedFiles;
using FormFactory.Attributes;
using FormFactory.Example.Models;

namespace FormFactory.Example.Controllers
{
    public class UploadedFilesController : Controller
    {
    //    static readonly MemoryCache Store = new MemoryCache("UploadedFilesStore");
    //    internal static UploadedFile UploadFile(HttpPostedFileBase file, ControllerContext controllerContext, ModelBindingContext modelBindingContext)
    //    {
    //        var type = modelBindingContext.ModelMetadata.DataTypeName ?? "Default";
    //        var filepath = type + "\\" + Guid.NewGuid().ToString().Replace("-", "") +
    //                       "\\" + Path.GetFileName(file.FileName);
    //        Store.Add(filepath, file, DateTimeOffset.Now.AddSeconds(10));
    //        return new UploadedFile
    //                   {
    //                       ContentLength = file.ContentLength,
    //                       ContentType = file.ContentType,
    //                       FileName = file.FileName,
    //                       Id = "/UploadedFiles?path=" + filepath
    //                   };
    //    }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Files(string id)
        {
            var file = FileStores.AppDataFileStore.GetById(id);
            return File(file.GetStream(), file.ContentType, file.FileName);
        }


        [HttpPost]
        public ActionResult UploadTest([FormModel] UploadedFilesTestModel model)
        {
            if (ModelState.IsValid)
            {
                var results = new UploadedFilesResultModel
                                  {
                                      Image1Url = model.Image1 != null ? ("/uploadedfiles/files/" + model.Image1.Id) : null,
                                      Image2Url = model.Image2 != null ? ("/uploadedfiles/files/" + model.Image2.Id) : null
                                  };
                return View("Index", results);
            }
            return View("Index");
        }
    }
}
