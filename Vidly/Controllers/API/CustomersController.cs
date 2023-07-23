using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _context.Customers;
            var recordsTotal = customers.Count();
            var jsonData = new { recordsTotal, data = customers };

            return Ok(jsonData);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }   

            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        [HttpPost]
        public IActionResult CreateCustomer(CustomerDto customerDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = _mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            //return created status code
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(CustomerDto customerDto, int id)
        {
            if (!ModelState.IsValid)
            {
                throw new BadHttpRequestException("not valid");
            }

            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer != null)
            {
                var newCustomer = _mapper.Map<Customer>(customerDto);
                _context.SaveChanges();
                //return updated customer
                return Ok(newCustomer);
            }

            throw new HttpRequestException("not valid");
        }

        [HttpDelete("{id}")]
        public void DeleteCustomer(int id )
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                throw new HttpRequestException("not valid");
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

        }
    }
}
