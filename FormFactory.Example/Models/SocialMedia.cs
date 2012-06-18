using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormFactory.Example.Models
{
    public class SocialMedia : ContactMethod
    {
        public SocialMedia()
        {
            SocialMediaType = new Twitter();
        }
        [DataType("Radio")]
        public SocialMediaType SocialMediaType { get; set; }   
        public IEnumerable<SocialMediaType> SocialMediaType_choices()
        {
            yield return SocialMediaType is Twitter ? SocialMediaType.Selected() : new Twitter();
            yield return SocialMediaType is Facebook ? SocialMediaType.Selected() : new Facebook();

        }
    }
}