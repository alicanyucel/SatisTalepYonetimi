using MediatR;
using Microsoft.EntityFrameworkCore;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.PurchaseQuotes.GetQuotesBySalesRequest
{
    internal sealed class GetQuotesBySalesRequestQueryHandler(
        IPurchaseQuoteRepository purchaseQuoteRepository) : IRequestHandler<GetQuotesBySalesRequestQuery, Result<List<PurchaseQuote>>>
    {
        public async Task<Result<List<PurchaseQuote>>> Handle(GetQuotesBySalesRequestQuery request, CancellationToken cancellationToken)
        {
            var quotes = await purchaseQuoteRepository
                .GetAll()
                .Include(p => p.Supplier)
                .Where(p => p.SalesRequestId == request.SalesRequestId)
                .ToListAsync(cancellationToken);
            return quotes;
        }
    }
}
