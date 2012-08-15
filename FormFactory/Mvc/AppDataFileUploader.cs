using System;
using System.IO;
using System.Web;

namespace FormFactory.Mvc
{
    internal class AppDataFileUploader
    {
        public static string Upload(bool modelStateIsValid, HttpPostedFileBase file)
        {
            if (!modelStateIsValid)
            {
                return null;
            }
            var folder = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(),
                                      "UploadedFiles");
            Directory.CreateDirectory(folder);

            int offset = 0;
            string filePath;
            do
            {
                filePath = folder + "\\" + GetOffsetFileName(file.FileName, offset);
                offset++;
            } while (File.Exists(filePath));
            
            file.SaveAs(filePath);
            return filePath;
        }
        static string GetOffsetFileName(string fileName, int offset)
        {
            if (offset == 0) return fileName;
            var parts = fileName.Split('.');
            parts[Math.Max(parts.Length - 2, 0)] += " (" + offset + ")";
            return string.Join(".", parts);
        }
    }
}