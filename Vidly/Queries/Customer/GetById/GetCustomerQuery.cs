using MediatR;
using Vidly.Models;

namespace Vidly.Queries;


public record GetCustomerQuery(int customerId) : IRequest<Customer>;