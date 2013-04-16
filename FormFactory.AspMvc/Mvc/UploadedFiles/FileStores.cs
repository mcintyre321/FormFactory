using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using FormFactory.ValueTypes;

namespace FormFactory.AspMvc.Mvc.UploadedFiles
{
    public class FileStores
    {
        public static UploadedFile AppData(HttpPostedFileBase httpPostedFileBase, ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            var dir = controllerContext.HttpContext.Server.MapPath("~/App_Data/UploadedFiles");
            Directory.CreateDirectory(dir);
            var directoryName = Guid.NewGuid().ToString();
            var relativePath = Path.Combine(directoryName, Path.GetFileName(httpPostedFileBase.FileName));
            var fullPath = Path.Combine(dir, relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            httpPostedFileBase.SaveAs(fullPath);
            return new UploadedFile()
            {
                ContentLength = httpPostedFileBase.ContentLength,
                ContentType = httpPostedFileBase.ContentType,
                FileName = relativePath,
                Id = directoryName
            };
        }
    }
}