using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormFactory.Example.Models.Examples
{
    public class ListRenderingExample
    {
        public ListRenderingExample()
        {
            
            RestrictedMaterials = new[] {"Guns", "Explosives"};

        }

        public IEnumerable<Hobby> Hobbies
        {
            get
            {
                yield return new Hobby("Swimming", 1);
                yield return new Hobby("Knitting", 4);
            }
        }

        //the interface model binder will bind IEnumerable<T> to T[]
        public IEnumerable<string> RestrictedMaterials { get; set; }
        //settable IEnumerable<strings> with choices get rendered as multi-selects.
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