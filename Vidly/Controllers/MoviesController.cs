using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers;

public class MoviesController : Controller
{
    public IActionResult Random()
    {
        var movie = new Movie() { Name = "Shrek!" };
        var customers = new List<Customer>
        {
            new Customer { Name = "Customer 1" },
            new Customer { Name = "Customer 2" }
        };
        var viewModel = new RandomMovieViewModel
        {
            Movie = movie,
            Customers = customers
        };
        return View(viewModel);  
    }
    
    public IActionResult Movies()
    {
       var movies = new List<Movie>
       {
           new Movie { Name = "Shrek!" },
           new Movie { Name = "Wall-e" }
       };
       var viewModel = new MoviesViewModel
       {
           Movies = movies
       };
       return View(viewModel);
    }
    
    public IActionResult Customers()
    {
        var customers = new List<Customer>
        {
            new ()
            {
                Name = "Customer 1",
                Id = 1
            },
            new ()
            {
                Name = "Customer 2",
                Id = 2
            }
        };
        var viewModel = new CustomersViewModel
        {
            Customers = customers
        };
        return View(viewModel);
    }
    
    [Route("Customers/Details/{id}")]
    public IActionResult Details(int id)
    {
        var customers = new List<Customer>
        {
            new()
            {
                Name = "Customer 1",
                Id = 1
            },
            new()
            {
                Name = "Customer 2",
                Id = 2
            }
        };
        var viewModel = new CustomersViewModel
        {
            Customers = customers.Where(c => c.Id == id).ToList()
        };
        return View(viewModel);
    }
    
}