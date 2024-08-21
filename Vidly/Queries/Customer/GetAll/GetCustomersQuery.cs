using MediatR;
using Vidly.Models;

namespace Vidly.Queries;


public record GetCustomersQuery() : IRequest<List<Customer>>;