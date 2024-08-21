using AutoMapper;
using MediatR;
using Vidly.Commands;
using Vidly.Models;

namespace Vidly.CommandHandlers;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
{
    private readonly AppDbContext _context;

    private readonly IMapper _mapper;
    public UpdateCustomerCommandHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer =await _context.Customers.FindAsync(request.Id , cancellationToken);
        if (customer == null)
        {
            return null;
        }
        _mapper.Map(request, customer);
        await _context.SaveChangesAsync(cancellationToken);
        return customer;
    }
}