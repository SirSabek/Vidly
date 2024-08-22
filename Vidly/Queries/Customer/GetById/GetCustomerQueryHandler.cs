using MediatR;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;

namespace Vidly.Queries;

public class GetCustomerQueryHandler(AppDbContext context) : IRequestHandler<GetCustomerQuery, Customer>
{
    private readonly AppDbContext _context = context;
    public async Task<Customer> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .Include(m => m.MembershipType)
            .FirstOrDefaultAsync(m => m.Id == request.customerId);

        if (customer == null)
        {
            return null;
        }

        return customer;
    }
}