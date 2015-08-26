using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FormFactory.Attributes;

namespace FormFactory.Example.Models.Examples
{
    public class NestedFormsExample { 

        public PhoneModel PhoneModel  { get; set; }
        public IEnumerable<PhoneModel> PhoneModel_choices()
        {
            //this could be a dynamic data source
            var data = new[]
            {
                new {Company = "Apple", Models = new[] { "iPhone 5s", "iPhone 6"}},
                new {Company = "Sony", Models = new[] { "Z3", "Z3 Compact"}}
            };

            return data.Select(i => new PhoneModel()
            {
                Company = i.Company,
                ModelChoices = i.Models
            }.DisplayName(i.Company));
        }

        
    }

    public class PhoneModel
    {
        
        [DataType("Hidden")] //We hide this field as it will have been displayed in the select list
        public string Company { get; set; }

        public string Model { get; set; }
        internal IEnumerable<string> ModelChoices;
        public IEnumerable<string> Model_choices()
        {
            return ModelChoices;
        }
    }


}