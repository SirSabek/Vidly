using MediatR;
using Vidly.Models;

namespace Vidly.Commands;
public record CreateCustomerCommand(
    string Name,
    bool IsSubscribedToNewsLetter,
    byte MembershipTypeId,
    DateTime? BirthDate
) : IRequest<Customer>;

