using System.ComponentModel.DataAnnotations;
using FormFactory.AspMvc.UploadedFiles;

namespace FormFactory.AspMvc.Example.Models
{
    public class UploadedFilesTestModel
    {
        [Required]
        public UploadedFile Image1 { get; set; }
        [DataType("Artwork")]
        public UploadedFile Image2 { get; set; }
    }
}