using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FormFactory.Example.Models
{
    public class RegisterModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords did not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Title")]
        public Titles? Title { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Organisation")]
        public string Organisation { get; set; }
    }
}