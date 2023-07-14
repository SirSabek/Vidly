using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers;

public class MoviesController : Controller
{
    private readonly AppDbContext _context;

    public MoviesController(AppDbContext context)
    {
        _context = context;
    }

    protected override void Dispose(bool disposing)
    {
       _context.Dispose();
    }

    public IActionResult Index()
    {
        var movies = _context.Movies.ToList();
        return View(movies);
    }
    [Route("movies/details/{id}")]
    public IActionResult Details(int id)
    {
        var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
        if (movie == null)
            return NotFound();
        return View(movie);
    }

    public IActionResult AddNewMovie()
    {
        var genres = new List<Genre>
        {
            Genre.Action,
            Genre.Comedy,
            Genre.Family,
            Genre.Romance,
            Genre.Horror
        };
        var viewModel = new MovieFormViewModel
        {
            Movie = new Movie(),
            Genres = genres
        };
        return View(viewModel);
    }
    
    [HttpPost]
    public IActionResult Save(Movie movie)
    {
        if (movie.Id == 0)
        {
            _context.Movies.Add(movie);
        }
        else
        {
            var movieInDb = _context.Movies.FirstOrDefault(m => m.Id == movie.Id);
            movieInDb.Name = movie.Name;
            movieInDb.Genre = movie.Genre;
            movieInDb.ReleaseDate = movie.ReleaseDate;
            movieInDb.DateAdded = movie.DateAdded;
            movieInDb.NumberInStock = movie.NumberInStock;
        }
        _context.SaveChanges();
        return RedirectToAction("Index", "Movies");
    }
    
    public IActionResult Edit(int id)
    {
        var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
        if (movie == null)
            return NotFound();
        var viewModel = new MovieFormViewModel
        {
            Movie = movie,
            Genres = new List<Genre>
            {
                Genre.Action,
                Genre.Comedy,
                Genre.Family,
                Genre.Romance,
                Genre.Horror
            }
        };
        return View("AddNewMovie", viewModel);
    }

}