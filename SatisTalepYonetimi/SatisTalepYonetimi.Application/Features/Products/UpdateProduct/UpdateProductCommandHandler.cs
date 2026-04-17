using AutoMapper;
using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Products.UpdateProduct
{
    internal sealed class UpdateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdateProductCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
            if (product is null)
                return (500, "Ürün bulunamadı");

            mapper.Map(request, product);
            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Ürün başarıyla güncellendi";
        }
    }
}
