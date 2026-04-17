using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SatisTalepYonetimi.Domain.Enums;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.PurchaseQuotes.SubmitQuotesForApproval
{
    internal sealed class SubmitQuotesForApprovalCommandHandler(
        ISalesRequestRepository salesRequestRepository,
        IPurchaseQuoteRepository purchaseQuoteRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<SubmitQuotesForApprovalCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(SubmitQuotesForApprovalCommand request, CancellationToken cancellationToken)
        {
            var salesRequest = await salesRequestRepository.GetByExpressionAsync(p => p.Id == request.SalesRequestId, cancellationToken);
            if (salesRequest is null)
                return (500, "Satış talebi bulunamadı");

            if (salesRequest.StatusValue != SalesRequestStatusEnum.ProcurementPending.Value &&
                salesRequest.StatusValue != SalesRequestStatusEnum.QuotesCollected.Value)
                return (500, "Bu talep satınalma sürecinde değil");

            var quoteCount = await purchaseQuoteRepository
                .GetAll()
                .CountAsync(q => q.SalesRequestId == request.SalesRequestId, cancellationToken);

            if (quoteCount < 3)
                return (500, $"En az 3 teklif toplanmalıdır. Mevcut teklif sayısı: {quoteCount}");

            salesRequest.StatusValue = SalesRequestStatusEnum.ManagementApproval.Value;
            salesRequestRepository.Update(salesRequest);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Teklifler yönetim onayına gönderildi";
        }
    }
}
