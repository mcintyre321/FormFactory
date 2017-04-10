using System.Collections.Generic;
using System.ComponentModel;
using FormFactory.Attributes;

namespace FormFactory.Example.Models.Examples
{
    public class ListRenderingExample
    {
        public ListRenderingExample()
        {
            //These values will be pre-selected
            RestrictedMaterials = new[] {"Guns", "Explosives"};
        }

        [Description("settable IEnumerable<strings> with choices get rendered as multi-selects")]
        public IEnumerable<string> RestrictedMaterials { get; set; }

        public IEnumerable<string> RestrictedMaterials_choices()
        {
            return new[] { "Guns", "Knives", "Explosives", "Nuclear Waste", "Weaponised Viruses" };
        }

    }



    public class Hobby
    {
        public Hobby(string name, int years)
        {
            Name = name;
            Years = years;
        }

        public string Name { get; private set; }

        public int Years { get; private set; }
    }

}