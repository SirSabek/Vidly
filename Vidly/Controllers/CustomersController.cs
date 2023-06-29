using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;

namespace Vidly.Controllers;

public class CustomersController : Controller
{
    private readonly AppDbContext _context;

    public CustomersController(AppDbContext context)
    {
        _context = context;
    }
    
    protected override void Dispose(bool disposing)
    {
        _context.Dispose();
    }

    public IActionResult Index()
    {
        var customers = _context.Customers.Include(c => c.MembershipType).ToList();
        return View(customers);
    }
    
    [Route("customers/details/{id}")]
    public IActionResult Details(int id)
    {
        var customer = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);
        if (customer == null)
            return NotFound();
        return View(customer);
    }
}