using System;
using System.ComponentModel.DataAnnotations;

namespace FormFactory.Example.Models.Examples
{
    public class SimplePropertiesExample
    {
        public SimplePropertiesExample()
        {
            Enabled = true;
        }

        //readonly property
        public int DaysSinceStartOfYear { get { return DateTime.Today.DayOfYear; } }

        //writable property [
        [Required()]
        [StringLength(32, MinimumLength = 8)]
        public string Name { get; set; }

        public bool Enabled { get; set; }

    }
}