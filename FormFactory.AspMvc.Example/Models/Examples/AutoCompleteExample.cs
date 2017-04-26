using System.Collections.Generic;
using System.ComponentModel;
using FormFactory.Attributes;
using System.Globalization;
using System.Linq;

namespace FormFactory.AspMvc.Example.Models.Examples
{
    public class AutoCompleteExample
    {

        [Required]
        [Display(Prompt = "Type to find your location")]
        public string Location { get; set; }
        public IEnumerable<string> Location_suggestions()
        {
            return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(c => c.Name).Distinct();
        }

        [Required]
        [Display(Name = "What is your home country?", Prompt = "Type to find your location")]
        [Description("AJAX suggestions using [SuggestionsUrl(\"...someurl...\")]")]
        [SuggestionsUrl("/home/CountrySuggestions")]
        public string CountryViaAjax { get; set; }
    }
}