using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Products.UpdateProduct
{
    public sealed record UpdateProductCommand(
        Guid Id,
        string Name,
        string Code,
        string Description,
        decimal UnitPrice,
        string Unit,
        int StockQuantity,
        bool IsActive) : IRequest<Result<string>>;
}
