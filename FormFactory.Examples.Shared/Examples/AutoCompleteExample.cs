using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FormFactory.Attributes;

namespace FormFactory.Examples.Shared.Examples
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
        [System.ComponentModel.Description("AJAX suggestions using [SuggestionsUrl(\"...someurl...\")]")]
        [SuggestionsUrl("/home/CountrySuggestions")]
        public string CountryViaAjax { get; set; }
    }
}