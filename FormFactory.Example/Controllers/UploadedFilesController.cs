using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormFactory.Attributes;
using FormFactory.Example.Models;

namespace FormFactory.Example.Controllers
{
    public class UploadedFilesController : Controller
    {
        private static string GetAppDataPath(string filePath)
        {
            return Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(),
                                "UploadedFiles\\", filePath);
        }
        internal static string UploadFile(HttpPostedFileBase file)
        {
            var filepath = Guid.NewGuid().ToString().Replace("-", "") +
                           "\\" + Path.GetFileName(file.FileName);
            var storePath = GetAppDataPath(filepath);
            Directory.CreateDirectory(Path.GetDirectoryName(storePath));
            file.SaveAs(storePath);
            return "/UploadedFiles?path=" + filepath;
        }

        [HttpGet]
        public FileResult Index(string path)
        {
            var appDataPath = GetAppDataPath(path);
            var fileInfo = new FileInfo(appDataPath);
            // NOTE: this contentType could be wrong; demo purposes only
            return File(appDataPath, "image/" + fileInfo.Extension.Replace(".", ""));
        }


        [HttpGet]
        public ActionResult UploadTest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadTest([FormModel] UploadedFilesTestModel model)
        {
            if (ModelState.IsValid)
            {
                var results = new UploadedFilesResultModel
                                  {
                                      Image1Url = model.Image1 != null ? model.Image1.Url : null,
                                      Image2Url = model.Image2 != null ? model.Image2.Url : null
                                  };
                return View(results);
            }
            return View();
        }
    }
}
