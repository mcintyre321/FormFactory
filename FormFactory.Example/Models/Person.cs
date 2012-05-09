using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormFactory.Example.Models
{
    public class Person
    {
        DateTime _dateOfBirth;

        public Person(DateTime dateOfBirth, string[] hobbies)
        {
            _dateOfBirth = dateOfBirth;
            Hobbies = hobbies;
            Position = Models.Position.SeniorSubcontractor;
            Enabled = true;
        }

        //readonly property
        public int Age { get { return (int) Math.Floor((DateTime.Now - _dateOfBirth).Days/365.25); } }

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

    }

    public enum Position
    {
        Contractor,
        SeniorSubcontractor
    }
}