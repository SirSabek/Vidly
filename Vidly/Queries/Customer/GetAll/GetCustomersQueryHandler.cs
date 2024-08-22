using MediatR;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;

namespace Vidly.Queries;

public class GetCustomersQueryHandler(AppDbContext context) : IRequestHandler<GetCustomersQuery, List<Customer>>
{
    private readonly AppDbContext _context = context;
    public async Task<List<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Customers.Include(m => m.MembershipType).ToListAsync();
    }
}