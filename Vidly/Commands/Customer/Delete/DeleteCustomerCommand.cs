using MediatR;
using Vidly.Models;

namespace Vidly.Commands;

public record DeleteCustomerCommand(
    int Id
) : IRequest<bool>;

