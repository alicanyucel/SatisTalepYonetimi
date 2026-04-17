using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Customers.CreateCustomer
{
    public sealed record CreateCustomerCommand(
        string Name,
        string Email,
        string Phone,
        string Address,
        string TaxNumber) : IRequest<Result<string>>;
}
