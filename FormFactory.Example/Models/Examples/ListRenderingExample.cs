using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormFactory.Example.Models.Examples
{
    public class ListRenderingExample
    {
        public ListRenderingExample()
        {
            TopMovies = new List<Movie>()
            {
                new Movie() {Title = "Fight Club"},
                new Movie() {Title = "Bambi"},
            };

            RestrictedMaterials = new[] { "Guns", "Explosives" };

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

        //Writable ICollections get rendered as re-orderable lists
        public ICollection<Movie> TopMovies { get; set; }
    }

    public class Movie
    {
        [Required]
        [StringLength(64, MinimumLength = 2)]
        public string Title { get; set; }
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