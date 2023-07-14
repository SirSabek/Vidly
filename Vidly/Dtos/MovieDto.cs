using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public string Name { get; set; }
        [Required]
        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [Range(1, 20)]
        public int NumberInStock { get; set; }
    }
}
