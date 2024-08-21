using AutoMapper;
using MediatR;
using Vidly.Commands;
using Vidly.Models;

namespace Vidly.CommandHandlers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Customer>(request);
        await _context.Customers.AddAsync(customer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return customer;
    }

}
