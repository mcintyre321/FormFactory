using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FormFactory.AspMvc.UploadedFiles;

namespace FormFactory.Example.Models
{
    public class UploadedFilesTestModel
    {
        [Required]
        public UploadedFile Image1 { get; set; }
        [DataType("Artwork")]
        public UploadedFile Image2 { get; set; }
    }
}