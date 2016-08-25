using System.Collections.Generic;
using System.ComponentModel;

namespace FormFactory.Example.Models.Examples
{
    public class ChoicesExample
    {
        [Description("By creating a method called {property}_choices you can create select lists")]
        public string Gender { get; set; }
        public IEnumerable<string> Gender_choices()
        {
            return "male,female,not specified".Split(',');
        }
    }
}