using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FormFactory.Example.Models.Examples
{
    public class EditableCollectionsExample
    {

        public EditableCollectionsExample()
        {
            TopMovies = new List<Movie>() {
                new Movie() {
                    Title = "Fight Club",
                    Stars = new List<Actor>() { 
                    new Actor() { Name = "Brad Pitt" },
                    new Actor() { Name = "Edward Norton" }
                    }
                },
                new Movie() {
                    Title = "The Silent Partner",
                    Stars = new List<Actor>() {
                    new Actor() { Name = "Elliott Gould" }
                    }
                },
                new Movie() {
                    Title = "Bambi",
                    Stars = new List<Actor>() 
                }
            };
        }

        [Description("Writable ICollections get rendered as re-orderable lists")]
        public ICollection<Movie> TopMovies { get; set; }
    }

    public class Movie
    {
        [Required]
        [StringLength(64, MinimumLength = 2)]
        public string Title { get; set; }
        public ICollection<Actor> Stars { get; set; }
    }

    public class Actor
    {
        [Required]
        [StringLength(64, MinimumLength = 2)]
        public string Name { get; set; }
    }
}