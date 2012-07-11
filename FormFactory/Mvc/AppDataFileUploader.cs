using System;
using System.IO;
using System.Web;

namespace FormFactory.Mvc
{
    internal class AppDataFileUploader
    {
        public static string Upload(HttpPostedFileBase file)
        {
            var filepath = Guid.NewGuid().ToString().Replace("-", "") +
                           "\\" + Path.GetFileName(file.FileName);
            var storePath = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(),
                                         "UploadedFiles\\", filepath);
            Directory.CreateDirectory(Path.GetDirectoryName(storePath));
            file.SaveAs(storePath);
            return filepath;
        }
    }
}