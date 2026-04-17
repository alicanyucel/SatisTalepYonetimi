using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Suppliers.DeleteSupplier
{
    public sealed record DeleteSupplierCommand(Guid Id) : IRequest<Result<string>>;
}
