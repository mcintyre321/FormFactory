using System.ComponentModel.DataAnnotations;
using FormFactory.Attributes;

namespace FormFactory.Example.Models
{
    public class LogInModel
    {
        [Required]
        [NoLabel, Display(Prompt="Email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [NoLabel, Display(Prompt="Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [LabelOnRight]
        public bool RememberMe { get; set; }
    }
}