using System.Collections.Generic;
using FormFactory.Attributes;

namespace FormFactory.AspMvc.Example.Models.Examples
{
    public class NestedFormsExample2
    {
        public NestedFormsExample2()
        {
            //This data will be preselected by use of the .Selected() extension method
            ContactMethod = new SocialMedia() {SocialMediaType = new SocialMediaType.Facebook()};
        }

        public ContactMethod ContactMethod { get; set; }
        //you can use objects as choices to create complex nested menus
        public IEnumerable<ContactMethod> ContactMethod_choices()
        {
            yield return ContactMethod is NoContactMethod ? ContactMethod.Selected() : new NoContactMethod();
            yield return ContactMethod is SocialMedia ? ContactMethod.Selected() : new SocialMedia();
            yield return ContactMethod is PhoneContactMethod ? ContactMethod.Selected() : new PhoneContactMethod();
        }
    }


    public class NoContactMethod : ContactMethod
    {
    }

    public class PhoneContactMethod : ContactMethod
    {
        public PhoneContactMethod()
        {
            Type = new PhoneType.Landline();
        }

        public string Number { get; set; }

        public PhoneType Type { get; set; }

        public IEnumerable<PhoneType> Type_choices()
        {
            yield return Type is PhoneType.Mobile ? Type : new PhoneType.Mobile();
            yield return Type is PhoneType.Landline ? Type : new PhoneType.Landline();

        }

    }

    public class PhoneType
    {
        public class Mobile : PhoneType
        {
            public string Provider { get; set; }

            public IEnumerable<string> Provider_suggestions()
            {
                return "Orange,TMobile,Three".Split(',');
            }
        }

        public class Landline : PhoneType
        {
            public string Provider { get; set; }

            public IEnumerable<string> Provider_suggestions()
            {
                return "TalkTalk,BT".Split(',');
            }
        }
    }

    public class SocialMedia : ContactMethod
    {
        public SocialMedia()
        {
            SocialMediaType = new SocialMediaType.Twitter();
        }

        [Radio]
        public SocialMediaType SocialMediaType { get; set; }

        public IEnumerable<SocialMediaType> SocialMediaType_choices()
        {
            yield return SocialMediaType is SocialMediaType.Twitter ? 
                SocialMediaType.Selected() : new SocialMediaType.Twitter();
            yield return SocialMediaType is SocialMediaType.Facebook ? 
                SocialMediaType.Selected() : new SocialMediaType.Facebook();
        }
    }

    public abstract class SocialMediaType
    {
        public class Twitter : SocialMediaType
        {
            [Required, Display(Prompt="@yourhandle")]
            public string TwitterHandle { get; set; }
        }

        public class Facebook : SocialMediaType
        {
            [Required]
            public string FacebookName { get; set; }
        }
    }
}