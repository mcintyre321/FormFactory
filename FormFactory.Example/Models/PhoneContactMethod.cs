using System.Collections.Generic;

namespace FormFactory.Example.Models
{
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
}