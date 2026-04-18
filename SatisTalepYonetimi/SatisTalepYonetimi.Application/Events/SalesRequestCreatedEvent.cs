namespace SatisTalepYonetimi.Application.Events;

public sealed record SalesRequestCreatedEvent(
    Guid SalesRequestId,
    string RequestNumber,
    Guid CustomerId,
    Guid RequestedByUserId,
    decimal TotalAmount,
    DateTime OccurredAt);
