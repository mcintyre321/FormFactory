using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FormFactory.Example.Models
{
    public class Person
    {
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public Person(DateTime dateOfBirth, string[] hobbies)
        {
            DateOfBirth = dateOfBirth;
            Hobbies = hobbies;
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

        //nullable enumerable property
        public Position? Position { get; set; }

        public bool Enabled { get; set; }

        //readonly property
        public IEnumerable<string> Hobbies { get; private set; }

        public string Gender { get; set; }
        //choices for geneder rendered as a select list
        public IEnumerable<string> Gender_choices() 
        {
            return "male,female,not specified".Split(',');
        }

        [Required]
        public string Location { get; set; }
        public IEnumerable<string> Location_suggestions()
        {
            return "USA,UK,Canada".Split(',');
        }

        public ContactMethod ContactMethod { get; set; }
        public IEnumerable<ContactMethod> ContactMethod_choices()
        {
            yield return new NoContactMethod();
            yield return new SocialMedia().Selected();
            yield return new PhoneContactMethod();

        }

        public ICollection<Movie> TopMovies { get; set; }

        public IEnumerable<string> RestrictedMaterials { get; set; }
        public IEnumerable<string> RestrictedMaterials_choices()
        {
            return new[] {"Guns", "Knives", "Explosives", "Nuclear Waste", "Weaponised Viruses"};
        } 
    }
}