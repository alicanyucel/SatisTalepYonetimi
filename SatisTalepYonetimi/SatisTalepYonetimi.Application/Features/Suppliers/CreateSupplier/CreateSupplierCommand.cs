using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Suppliers.CreateSupplier
{
    public sealed record CreateSupplierCommand(
        string Name,
        string ContactPerson,
        string Email,
        string Phone,
        string Address,
        string TaxNumber) : IRequest<Result<string>>;
}
