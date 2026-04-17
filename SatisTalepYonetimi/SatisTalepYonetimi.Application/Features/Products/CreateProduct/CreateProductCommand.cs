using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Products.CreateProduct
{
    public sealed record CreateProductCommand(
        string Name,
        string Code,
        string Description,
        decimal UnitPrice,
        string Unit,
        int StockQuantity) : IRequest<Result<string>>;
}
