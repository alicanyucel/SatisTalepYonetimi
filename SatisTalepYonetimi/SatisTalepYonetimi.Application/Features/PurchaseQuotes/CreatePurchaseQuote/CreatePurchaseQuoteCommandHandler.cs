using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Enums;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.PurchaseQuotes.CreatePurchaseQuote
{
    internal sealed class CreatePurchaseQuoteCommandHandler(
        IPurchaseQuoteRepository purchaseQuoteRepository,
        ISalesRequestRepository salesRequestRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreatePurchaseQuoteCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreatePurchaseQuoteCommand request, CancellationToken cancellationToken)
        {
            var salesRequest = await salesRequestRepository.GetByExpressionAsync(p => p.Id == request.SalesRequestId, cancellationToken);
            if (salesRequest is null)
                return (500, "Satış talebi bulunamadı");

            if (salesRequest.StatusValue != SalesRequestStatusEnum.ProcurementPending.Value &&
                salesRequest.StatusValue != SalesRequestStatusEnum.QuotesCollected.Value)
                return (500, "Bu talep satınalma sürecinde değil");

            var quote = new PurchaseQuote
            {
                SalesRequestId = request.SalesRequestId,
                SupplierId = request.SupplierId,
                TotalAmount = request.TotalAmount,
                Note = request.Note,
                QuoteDate = DateTime.UtcNow
            };

            await purchaseQuoteRepository.AddAsync(quote, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Teklif başarıyla eklendi";
        }
    }
}
