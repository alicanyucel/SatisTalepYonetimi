namespace SatisTalepYonetimi.Application.Events;

public sealed record PurchaseQuoteApprovedEvent(
    Guid QuoteId,
    Guid SalesRequestId,
    Guid SupplierId,
    decimal Amount,
    DateTime OccurredAt);
