using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FormFactory.Attributes;

namespace FormFactory.Example.Models.Examples
{
    public class EasyAutoCompleteExample
    {

        [Required]
        [Placeholder("Type to find your location")]
        public string Location { get; set; }
        public IEnumerable<string> Location_suggestions()
        {
            return "USA,UK,Canada".Split(',');
        }

        [Required]
        [DisplayName("Countries (via ajax)")]
        [Placeholder(" via ajax using [SuggestionsUrl(\"...someurl...\")]")]
        [SuggestionsUrl("/home/CountrySuggestions")]
        public string CountryViaAjax { get; set; }
    }
}