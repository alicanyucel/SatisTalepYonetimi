using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Customers.DeleteCustomer
{
    public sealed record DeleteCustomerCommand(Guid Id) : IRequest<Result<string>>;
}
