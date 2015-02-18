using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FormFactory.Attributes;

namespace FormFactory.Example.Models
{
    public class Person
    {
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public Person()
        {
        }
        public Person(DateTime dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
            Position = Models.Position.SeniorSubcontractor;
            Enabled = true;
            TopMovies = new List<Movie>()
            {
                new Movie() {Title = "Fight Club"},
                new Movie() {Title = "Bambi"},

            };
            RestrictedMaterials = new[] {"Guns", "Explosives"};
        }

        //readonly property
        public int Age { get { return (int) Math.Floor((DateTime.Now - DateOfBirth).Days/365.25); } }

        //writable property [
        [Required()][StringLength(32, MinimumLength = 8)]
        public string Name { get; set; }

        [Description("Enums are rendered as dropdowns - nullable ones have an empty option")]
        public Position? Position { get; set; }


        public bool Enabled { get; set; }

        public IEnumerable<Hobby> Hobbies { get
        {
            yield return new Hobby("Swimming", 1) ;
            yield return new Hobby("Knitting", 4);

        }}

        public string Gender { get; set; }
        //choices for geneder rendered as a select list
        public IEnumerable<string> Gender_choices() 
        {
            return "male,female,not specified".Split(',');
        }

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

        public ContactMethod ContactMethod { get; set; }
        //you can use objects as choices to create complex nested menus
        public IEnumerable<ContactMethod> ContactMethod_choices() 
        {
            yield return ContactMethod is NoContactMethod ? ContactMethod.Selected() : new NoContactMethod();
            yield return ContactMethod is SocialMedia ? ContactMethod.Selected() : new SocialMedia();
            yield return ContactMethod is PhoneContactMethod ? ContactMethod.Selected() : new PhoneContactMethod();
        }

        //ICollections get rendered as re-orderable lists
        public ICollection<Movie> TopMovies { get; set; }

        //the interface model binder will bind IEnumerable<T> to T[]
        public IEnumerable<string> RestrictedMaterials { get; set; }
        //settable IEnumerable<strings> with choices get rendered as multi-selects.
        public IEnumerable<string> RestrictedMaterials_choices()
        {
            return new[] {"Guns", "Knives", "Explosives", "Nuclear Waste", "Weaponised Viruses"};
        }

        [Required]
        [SuggestionsUrl("/home/SearchUsers")]
        public Guid? AutocompleteReturningGuid { get; set; }
    }


    public class Hobby
    {
        public Hobby(string name, int years)
        {
            Name = name;
            Years = years;
        }

        public string Name { get; private set; }

        public int Years { get; private set; }
    }
}