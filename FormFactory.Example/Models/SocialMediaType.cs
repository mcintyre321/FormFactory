using System.ComponentModel.DataAnnotations;
using FormFactory.Attributes;

namespace FormFactory.Example.Models
{
    public abstract class SocialMediaType
    {

    }

    public class Twitter : SocialMediaType
    {
        [Required][Placeholder("@yourhandle")]
        public string TwitterHandle { get; set; }
    }
    public class Facebook : SocialMediaType
    {
        [Required]
        public string FacebookName { get; set; }
    }

}