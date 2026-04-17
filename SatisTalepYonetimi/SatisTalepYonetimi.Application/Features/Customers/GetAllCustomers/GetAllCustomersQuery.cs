using MediatR;
using SatisTalepYonetimi.Domain.Entities;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Customers.GetAllCustomers
{
    public sealed record GetAllCustomersQuery() : IRequest<Result<List<Customer>>>;
}
