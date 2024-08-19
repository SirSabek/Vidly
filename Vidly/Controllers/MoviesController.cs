using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers;

[Authorize]
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
        //var movies = _context.Movies.Include(m => m.Genre).ToList();
        return View();
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
        var genres = _context.Genres.ToList();
        var viewModel = new MovieFormViewModel
        {
            Movie = new Movie(),
            Genres = genres
        };
        return View("MovieForm",viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Save(Movie movie)
    {
        ModelState.Remove("Movie.Genre");
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        if (movie.Id == 0)
        {
            _context.Movies.Add(movie);
        }
        else
        {
            var movieInDb = _context.Movies.FirstOrDefault(m => m.Id == movie.Id);
            movieInDb.Name = movie.Name;
            movieInDb.GenreId = movie.GenreId;
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
        {
            return NotFound();
        }

        var viewModel = new MovieFormViewModel
        {
            Movie = movie,
            Genres = _context.Genres.ToList()
        };
        return View("MovieForm", viewModel);
    }

}