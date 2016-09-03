using System.ComponentModel.DataAnnotations;
using FormFactory.Attributes;

namespace FormFactory.AspMvc.Example.Models
{
    public class LogInModel
    {
        [Required]
        [NoLabel, Placeholder("Email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [NoLabel, Placeholder("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [LabelOnRight]
        public bool RememberMe { get; set; }
    }
}