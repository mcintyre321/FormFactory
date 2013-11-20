using System;
using System.IO;

namespace FormFactory.AspMvc.UploadedFiles
{
    public class UploadedFile
    {
         
        public string ContentType { get; set; }
        public string Id { get; set; }
        public int ContentLength { get; set; }
        public string FileName { get; set; }
         
    }

}