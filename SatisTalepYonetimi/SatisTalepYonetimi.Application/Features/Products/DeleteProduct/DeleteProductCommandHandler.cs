using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Products.DeleteProduct
{
    internal sealed class DeleteProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
            if (product is null)
                return (500, "Ürün bulunamadı");

            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Ürün başarıyla silindi";
        }
    }
}
