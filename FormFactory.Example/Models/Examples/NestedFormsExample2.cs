using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FormFactory.Attributes;

namespace FormFactory.Example.Models.Examples
{
    public class NestedFormsExample2 {

        public NestedFormsExample2()
        {
            //This data will be preselected by use of the .Selected() extension method
            ContactMethod = new PhoneContactMethod() {Number = "0845 50 50 50", Type = new Mobile()};
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
            Type = new Landline();
        }
        public string Number { get; set; }

        public PhoneType Type { get; set; }

        public IEnumerable<PhoneType> Type_choices()
        {
            yield return Type is Mobile ? Type : new Mobile();
            yield return Type is Landline ? Type : new Landline();

        }

    }

    public class PhoneType
    {

    }

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

    public abstract class SocialMediaType
    {

    }

    public class Twitter : SocialMediaType
    {
        [Required]
        [Placeholder("@yourhandle")]
        public string TwitterHandle { get; set; }
    }
    public class Facebook : SocialMediaType
    {
        [Required]
        public string FacebookName { get; set; }
    }
}