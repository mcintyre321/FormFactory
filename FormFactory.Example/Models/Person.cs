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
        }

        //readonly property
        public int Age { get { return (int) Math.Floor((DateTime.Now - DateOfBirth).Days/365.25); } }

        //writable property
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

        public string Location { get; set; }
        //location enhanced with auto complete
        public IEnumerable<string> Location_suggestions() 
        {
            return "USA,UK,Canada".Split(',');
        }

        public ContactMethod ContactMethod { get; set; }
        public IEnumerable<ContactMethod> ContactMethod_choices()
        {
            yield return null as ContactMethod;
            yield return new SocialMedia() .Selected();
            yield return new PhoneContactMethod();

        }

        public ICollection<Movie> TopMovies { get; set; } 
    }
}