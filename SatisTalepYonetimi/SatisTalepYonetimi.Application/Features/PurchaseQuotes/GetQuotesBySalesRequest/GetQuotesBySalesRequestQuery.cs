using MediatR;
using SatisTalepYonetimi.Domain.Entities;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.PurchaseQuotes.GetQuotesBySalesRequest
{
    public sealed record GetQuotesBySalesRequestQuery(Guid SalesRequestId) : IRequest<Result<List<PurchaseQuote>>>;
}
