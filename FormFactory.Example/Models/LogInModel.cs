using System.ComponentModel.DataAnnotations;
using FormFactory.Attributes;

namespace FormFactory.Example.Models
{
    public class LogInModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [LabelOnRight]
        public bool RememberMe { get; set; }
    }
}