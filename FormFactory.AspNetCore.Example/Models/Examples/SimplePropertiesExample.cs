using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FormFactory.Example.Models.Examples
{
    public class SimplePropertiesExample
    {
        public SimplePropertiesExample()
        {
            Enabled = true;
        }

        [Description("Readonly properties are rendered as readonly")]
        public int DaysSinceStartOfYear { get { return DateTime.Today.DayOfYear; } }

        [Required()]
        [StringLength(32, MinimumLength = 8)]
        [Description("Writable properties can use MVC validation attributes")]
        public string Name { get; set; }

        public bool Enabled { get; set; }

    }
}