using MediatR;
using SatisTalepYonetimi.Domain.Entities;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Products.GetAllProducts
{
    public sealed record GetAllProductsQuery() : IRequest<Result<List<Product>>>;
}
