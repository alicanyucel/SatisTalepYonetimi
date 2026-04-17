using AutoMapper;
using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Products.CreateProduct
{
    internal sealed class CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateProductCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await productRepository.GetByExpressionAsync(p => p.Code == request.Code, cancellationToken);
            if (existingProduct is not null)
                return (500, "Bu ürün kodu zaten mevcut");

            var product = mapper.Map<Product>(request);
            await productRepository.AddAsync(product, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Ürün başarıyla oluşturuldu";
        }
    }
}
