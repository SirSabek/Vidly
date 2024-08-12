using System.ComponentModel.DataAnnotations;

namespace Vidly.Models;

public class Movie
{
    public int  Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public Genre Genre { get; set; }
    
    [Display(Name = "Genre")]
    [Required]
    public byte GenreId { get; set; }
    [Required]
    public DateTime ReleaseDate { get; set; }
    public DateTime DateAdded { get; set; }
    [Required]
    [Range(1, 20)]
    public int NumberInStock { get; set; }
}

public enum Genre
{
    Action = 1,
    Comedy,
    Family,
    Romance,
    Horror
}