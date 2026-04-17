using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.PurchaseQuotes.CreatePurchaseQuote
{
    public sealed record CreatePurchaseQuoteCommand(
        Guid SalesRequestId,
        Guid SupplierId,
        decimal TotalAmount,
        string? Note) : IRequest<Result<string>>;
}
