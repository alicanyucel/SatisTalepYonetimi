using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.PurchaseQuotes.ApproveQuote
{
    public sealed record ApproveQuoteCommand(
        Guid SalesRequestId,
        Guid QuoteId) : IRequest<Result<string>>;
}
