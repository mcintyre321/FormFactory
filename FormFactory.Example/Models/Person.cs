using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormFactory.Example.Models
{
    public class Person
    {
        DateTime _dateOfBirth;
        public Person(DateTime dateOfBirth)
        {
            _dateOfBirth = dateOfBirth;
            Position = Models.Position.SeniorSubcontractor;
        }

        //readonly property
        public int Age { get { return (int) Math.Floor((DateTime.Now - _dateOfBirth).Days/365.25); } }

        //writable property
        public string Name { get; set; }

        //nullable enumerable property
        public Position? Position { get; set; }

        public bool Enabled { get; set; }


    }

    public enum Position
    {
        Contractor,
        SeniorSubcontractor
    }
}