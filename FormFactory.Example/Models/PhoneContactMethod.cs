using System.Collections.Generic;

namespace FormFactory.Example.Models
{
    public class PhoneContactMethod : ContactMethod
    {
        public string Name { get { return "Phone"; } }
        public string Number { get; set; }
        public string Type { get; set; }
        public IEnumerable<string> Type_choices()
        {
            return "Landline,Mobile".Split(',');
        } 

    }
}