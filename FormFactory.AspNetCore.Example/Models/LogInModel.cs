using FormFactory.Attributes;

namespace FormFactory.Example.Models
{
    public class LogInModel
    {
        [Required]
        [NoLabel, Display(Prompt="Email address")]
        [Email]
        public string Email { get; set; }

        [Required]
        [NoLabel, Display(Prompt="Password")]
        [Password]
        public string Password { get; set; }

        [LabelOnRight]
        public bool RememberMe { get; set; }
    }
}