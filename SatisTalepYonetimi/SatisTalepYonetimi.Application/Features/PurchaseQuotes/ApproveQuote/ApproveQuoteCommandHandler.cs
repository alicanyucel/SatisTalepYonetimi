using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Enums;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.PurchaseQuotes.ApproveQuote
{
    internal sealed class ApproveQuoteCommandHandler(
        ISalesRequestRepository salesRequestRepository,
        IPurchaseQuoteRepository purchaseQuoteRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<ApproveQuoteCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(ApproveQuoteCommand request, CancellationToken cancellationToken)
        {
            var salesRequest = await salesRequestRepository.GetByExpressionAsync(p => p.Id == request.SalesRequestId, cancellationToken);
            if (salesRequest is null)
                return (500, "Satış talebi bulunamadı");

            if (salesRequest.StatusValue != SalesRequestStatusEnum.ManagementApproval.Value)
                return (500, "Bu talep yönetim onayında değil");

            var quote = await purchaseQuoteRepository.GetByExpressionAsync(p => p.Id == request.QuoteId, cancellationToken);
            if (quote is null)
                return (500, "Teklif bulunamadı");

            if (quote.SalesRequestId != request.SalesRequestId)
                return (500, "Teklif bu satış talebine ait değil");

            quote.IsSelected = true;
            quote.ApprovedDate = DateTime.UtcNow;
            purchaseQuoteRepository.Update(quote);

            salesRequest.SelectedQuoteId = request.QuoteId;
            salesRequest.StatusValue = SalesRequestStatusEnum.QuoteApproved.Value;
            salesRequestRepository.Update(salesRequest);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Teklif onaylandı ve satış talebi tamamlandı";
        }
    }
}
