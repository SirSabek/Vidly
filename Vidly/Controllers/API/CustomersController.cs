using System.Net;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Commands;
using Vidly.Dtos;
using Vidly.Models;
using Vidly.Queries;

namespace Vidly.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public CustomersController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers( )
        {
            var query = new GetCustomersQuery();
            var customers =await _mediator.Send(query);
            var recordsTotal = customers.Count();
            var jsonData = new { recordsTotal, data = customers };

            return Ok(jsonData);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var query = new GetCustomerQuery(id);
            var result = await _mediator.Send(query);
            if (result is null)
            {
                return NotFound();
            }   
            return Ok(result);
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
