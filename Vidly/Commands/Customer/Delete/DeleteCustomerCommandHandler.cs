using AutoMapper;
using MediatR;
using Vidly.Commands;
using Vidly.Models;

namespace Vidly.CommandHandlers;

public class DeleteCustomerCommandHandler(AppDbContext context) : IRequestHandler<DeleteCustomerCommand, bool>
{
    private readonly AppDbContext _context = context;

    public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FindAsync(request.Id, cancellationToken);
        if (customer is not null)
        {
            _context.Remove(customer);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;
    }
}
