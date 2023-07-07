using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;
using Vidly.ViewModels;

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
    
    public IActionResult AddNewCustomer() 
    {
        var membershipTypes = _context.MembershipTypes.ToList();
        var viewModel = new CustomerFormViewModel
        {
            Customer = new Customer(),
            MembershipTypes = membershipTypes
        };
        return View(viewModel);
    }
    
    [HttpPost]
    public IActionResult Save(Customer customer)
    {
        if (customer.Id == 0)
        {
            _context.Customers.Add(customer);
        }
        else
        {
            var customerInDb = _context.Customers.FirstOrDefault(c => c.Id == customer.Id);
            customerInDb.Name = customer.Name;
            customerInDb.BirthDate = customer.BirthDate;
            customerInDb.MembershipType = customer.MembershipType;
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
        }

        _context.SaveChanges();
        return RedirectToAction("Index", "Customers");
    }

    public IActionResult Edit(int id)
    {
        var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
        if (customer == null)
        {
            return NotFound();
        }
        var viewModel = new CustomerFormViewModel
        {
            Customer = customer,
            MembershipTypes = _context.MembershipTypes.ToList()
        };
        return View("AddNewCustomer", viewModel);
    }
}