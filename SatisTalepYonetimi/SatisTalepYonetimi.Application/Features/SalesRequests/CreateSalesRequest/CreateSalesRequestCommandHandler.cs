using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.SalesRequests.CreateSalesRequest
{
    internal sealed class CreateSalesRequestCommandHandler(
        ISalesRequestRepository salesRequestRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateSalesRequestCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateSalesRequestCommand request, CancellationToken cancellationToken)
        {
            var salesRequest = new SalesRequest
            {
                RequestNumber = $"SR-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString()[..4].ToUpper()}",
                CustomerId = request.CustomerId,
                RequestedByUserId = request.RequestedByUserId,
                Note = request.Note,
                StatusValue = 1,
                RequestDate = DateTime.UtcNow,
                Items = []
            };

            decimal totalAmount = 0;

            foreach (var item in request.Items)
            {
                var product = await productRepository.GetByExpressionAsync(p => p.Id == item.ProductId, cancellationToken);
                if (product is null)
                    return (500, $"Ürün bulunamadı: {item.ProductId}");

                var lineTotal = product.UnitPrice * item.Quantity;
                totalAmount += lineTotal;

                salesRequest.Items.Add(new SalesRequestItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.UnitPrice,
                    TotalPrice = lineTotal
                });
            }

            salesRequest.TotalAmount = totalAmount;

            await salesRequestRepository.AddAsync(salesRequest, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Satış talebi başarıyla oluşturuldu";
        }
    }
}
