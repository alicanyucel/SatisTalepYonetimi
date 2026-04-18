namespace SatisTalepYonetimi.Application.Events;

public sealed record SalesRequestStatusChangedEvent(
    Guid SalesRequestId,
    string RequestNumber,
    int OldStatus,
    int NewStatus,
    Guid? CustomerId,
    DateTime OccurredAt);
