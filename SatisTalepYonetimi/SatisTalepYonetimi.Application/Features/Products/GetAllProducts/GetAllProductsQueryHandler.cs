using MediatR;
using Microsoft.EntityFrameworkCore;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Products.GetAllProducts
{
    internal sealed class GetAllProductsQueryHandler(
        IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, Result<List<Product>>>
    {
        public async Task<Result<List<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAll().ToListAsync(cancellationToken);
            return products;
        }
    }
}
