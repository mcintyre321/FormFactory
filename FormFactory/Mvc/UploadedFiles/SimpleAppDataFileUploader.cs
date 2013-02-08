using System;
using System.IO;
using System.Web;
using FormFactory.ValueTypes;

namespace FormFactory.Mvc.UploadedFiles
{
    public class SimpleAppDataFileUploader
    {
        public static TUploadedFile DoSave<TUploadedFile>(bool modelStateIsValid, HttpPostedFileBase file)
            where TUploadedFile : UploadedFile, new()
        {
            if (!modelStateIsValid)
            {
                return null;
            }
            var folder = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "UploadedFiles");
            Directory.CreateDirectory(folder);

            int offset = 0;
            string filePath;
            do
            {
                filePath = folder + "\\" + GetOffsetFileName(file.FileName, offset);
                offset++;
            } while (File.Exists(filePath));
            
            file.SaveAs(filePath);
            return new TUploadedFile
                       {
                           Id = filePath,
                           ContentLength = file.ContentLength,
                           ContentType = file.ContentType,
                           FileName = file.FileName
                       };
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