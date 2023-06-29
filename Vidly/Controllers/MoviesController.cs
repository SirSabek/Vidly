using Microsoft.AspNetCore.Mvc;
using Vidly.Models;

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
}