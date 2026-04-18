namespace SatisTalepYonetimi.Infrastructure.Sagas;

public sealed class SalesRequestSagaState : MassTransit.SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = string.Empty;
    public Guid SalesRequestId { get; set; }
    public string RequestNumber { get; set; } = string.Empty;
    public Guid? CustomerId { get; set; }
    public int StatusValue { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
