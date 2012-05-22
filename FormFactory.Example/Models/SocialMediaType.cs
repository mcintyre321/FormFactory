namespace FormFactory.Example.Models
{
    public abstract class SocialMediaType
    {

    }

    public class Twitter : SocialMediaType
    {
        public string TwitterHandle { get; set; }
    }
    public class Facebook : SocialMediaType
    {
        public string FacebookName { get; set; }
    }

}