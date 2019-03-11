using System;
using FormFactory.Attributes;

namespace FormFactory.Examples.Shared.Examples
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