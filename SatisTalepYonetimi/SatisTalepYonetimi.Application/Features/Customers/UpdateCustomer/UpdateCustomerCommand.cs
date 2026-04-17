using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Customers.UpdateCustomer
{
    public sealed record UpdateCustomerCommand(
        Guid Id,
        string Name,
        string Email,
        string Phone,
        string Address,
        string TaxNumber,
        bool IsActive) : IRequest<Result<string>>;
}
