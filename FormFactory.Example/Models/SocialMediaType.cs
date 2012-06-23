using System.ComponentModel.DataAnnotations;

namespace FormFactory.Example.Models
{
    public abstract class SocialMediaType
    {

    }

    public class Twitter : SocialMediaType
    {
        [Required]
        public string TwitterHandle { get; set; }
    }
    public class Facebook : SocialMediaType
    {
        [Required]
        public string FacebookName { get; set; }
    }

}