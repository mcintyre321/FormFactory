using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using FormFactory.ValueTypes;

namespace FormFactory.Mvc.UploadedFiles
{
    public class AppDataFileStore : IFileStore
    {

        public UploadedFile Store(HttpPostedFileBase httpPostedFileBase, ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            var dir = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/UploadedFiles");

            Directory.CreateDirectory(dir);
            var directoryName = Guid.NewGuid().ToString();
            var relativePath = Path.Combine(directoryName, Path.GetFileName(httpPostedFileBase.FileName));
            var fullPath = Path.Combine(dir, relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            httpPostedFileBase.SaveAs(fullPath);

            var uploadedFile = new UploadedFile()
            {
                ContentLength = httpPostedFileBase.ContentLength, ContentType = httpPostedFileBase.ContentType, FileName = Path.GetFileName(relativePath), Id = directoryName
            };
            var serializer = new JavaScriptSerializer();
            var metadata = serializer.Serialize(uploadedFile);
            File.WriteAllText(Path.Combine(StoreFolderPath, uploadedFile.Id, "metadata.json"), metadata);

            uploadedFile.SetGetStream(() => File.OpenRead(Path.Combine(StoreFolderPath, uploadedFile.Id, uploadedFile.FileName)));

            return uploadedFile;
        }

        public UploadedFile GetById(string id)
        {
            var serializer = new JavaScriptSerializer();
            var filename = Path.Combine(StoreFolderPath, id, "metadata.json");
            var uploadedFile = serializer.Deserialize<UploadedFile>(File.ReadAllText(filename));
            uploadedFile.SetGetStream(() => File.OpenRead(Path.Combine(StoreFolderPath, id, uploadedFile.FileName)));
            return uploadedFile;
        }

        private static string StoreFolderPath
        {
            get { return System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/UploadedFiles"); }
        }
    }
}