using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Commands;
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
        private readonly ISender _mediator;

        public CustomersController(AppDbContext context, IMapper mapper, ISender mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _context.Customers.Include( m=> m.MembershipType);
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
        public async Task <IActionResult> CreateCustomer(CreateCustomerCommand command) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = await _mediator.Send(command);
            //return created status code
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerCommand command)
        {
           if (id != command.Id)
            {
                return BadRequest("Customer ID mismatch.");
            }
            var customer = await _mediator.Send(command);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id, [FromBody]DeleteCustomerCommand command )
        {
             if (id != command.Id)
            {
                return BadRequest("Customer ID mismatch.");
            }

            var result =await _mediator.Send(command);

            if (result == false)
            {
                throw new HttpRequestException("not found");
            }

            return Ok("customer is deleted!");
        }
    }
}
