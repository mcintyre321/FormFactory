using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using FormFactory.Attributes;

namespace FormFactory.AspMvc.Example.Models.Examples
{
    public class AutoCompleteExample
    {

        [Required]
        [Placeholder("Type to find your location")]
        public string Location { get; set; }
        public IEnumerable<string> Location_suggestions()
        {
            return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(c => c.Name).Distinct();
        }

        [Required]
        [DisplayName("Countrie")]
        [Placeholder("Type to find your location")]
        [Description("AJAX suggestions using [SuggestionsUrl(\"...someurl...\")]")]
        [SuggestionsUrl("/home/CountrySuggestions")]
        public string CountryViaAjax { get; set; }
    }
}