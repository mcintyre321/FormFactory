using System.ComponentModel.DataAnnotations;

namespace FormFactory.Example.Models
{
    public class Movie
    {
        [Required][StringLength(64, MinimumLength = 2)]
        public string Title { get; set; }
    }
}