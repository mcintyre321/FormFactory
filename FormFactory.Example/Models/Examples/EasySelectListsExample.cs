using System.Collections.Generic;

namespace FormFactory.Example.Models.Examples
{
    public class EasySelectListsExample
    {
        public string Gender { get; set; }
        //choices for gender rendered as a select list
        public IEnumerable<string> Gender_choices()
        {
            return "male,female,not specified".Split(',');
        }
    }
}