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
        }

        //readonly property
        public int Age { get { return (int) Math.Floor((DateTime.Now - _dateOfBirth).Days/365.25); } }

        //writable property
        public string Name { get; set; }

        //enumerable property
        public string Roles { get; set; }

        public bool Enabled { get; set; }

    }
}