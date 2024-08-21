using MediatR;
using Vidly.Models;

namespace Vidly.Commands;

public record UpdateCustomerCommand(
    int Id,
    string Name,
    bool IsSubscribedToNewsLetter,
    byte MembershipTypeId,
    DateTime? BirthDate
) : IRequest<Customer>;

