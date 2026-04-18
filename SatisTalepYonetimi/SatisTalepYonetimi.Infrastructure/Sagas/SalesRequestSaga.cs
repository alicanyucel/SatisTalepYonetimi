using MassTransit;
using SatisTalepYonetimi.Application.Events;

namespace SatisTalepYonetimi.Infrastructure.Sagas;

public sealed class SalesRequestSaga : MassTransitStateMachine<SalesRequestSagaState>
{
    // States
    public State Created { get; private set; } = default!;
    public State ManagerApproval { get; private set; } = default!;
    public State ManagerApproved { get; private set; } = default!;
    public State ProcurementPending { get; private set; } = default!;
    public State QuotesCollected { get; private set; } = default!;
    public State ManagementApproval { get; private set; } = default!;
    public State QuoteApproved { get; private set; } = default!;
    public State Completed { get; private set; } = default!;
    public State Rejected { get; private set; } = default!;
    public State Cancelled { get; private set; } = default!;

    // Events
    public Event<SalesRequestCreatedEvent> SalesRequestCreated { get; private set; } = default!;
    public Event<SalesRequestStatusChangedEvent> StatusChanged { get; private set; } = default!;

    public SalesRequestSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => SalesRequestCreated, x => x.CorrelateById(ctx => ctx.Message.SalesRequestId));
        Event(() => StatusChanged, x => x.CorrelateById(ctx => ctx.Message.SalesRequestId));

        Initially(
            When(SalesRequestCreated)
                .Then(ctx =>
                {
                    ctx.Saga.SalesRequestId = ctx.Message.SalesRequestId;
                    ctx.Saga.RequestNumber = ctx.Message.RequestNumber;
                    ctx.Saga.CustomerId = ctx.Message.CustomerId;
                    ctx.Saga.StatusValue = 1;
                    ctx.Saga.CreatedAt = ctx.Message.OccurredAt;
                })
                .TransitionTo(Created));

        During(Created,
            When(StatusChanged)
                .Then(ctx => UpdateSagaState(ctx))
                .IfElse(ctx => ctx.Message.NewStatus == 2,
                    binder => binder.TransitionTo(ManagerApproval),
                    binder => binder.IfElse(ctx => ctx.Message.NewStatus == 10,
                        b => b.TransitionTo(Cancelled),
                        b => b.Then(_ => { }))));

        During(ManagerApproval,
            When(StatusChanged)
                .Then(ctx => UpdateSagaState(ctx))
                .IfElse(ctx => ctx.Message.NewStatus == 3,
                    binder => binder.TransitionTo(ManagerApproved),
                    binder => binder.IfElse(ctx => ctx.Message.NewStatus == 4,
                        b => b.TransitionTo(Rejected),
                        b => b.IfElse(ctx => ctx.Message.NewStatus == 10,
                            b2 => b2.TransitionTo(Cancelled),
                            b2 => b2.Then(_ => { })))));

        During(ManagerApproved,
            When(StatusChanged)
                .Then(ctx => UpdateSagaState(ctx))
                .IfElse(ctx => ctx.Message.NewStatus == 5,
                    binder => binder.TransitionTo(ProcurementPending),
                    binder => binder.IfElse(ctx => ctx.Message.NewStatus == 10,
                        b => b.TransitionTo(Cancelled),
                        b => b.Then(_ => { }))));

        During(ProcurementPending,
            When(StatusChanged)
                .Then(ctx => UpdateSagaState(ctx))
                .IfElse(ctx => ctx.Message.NewStatus == 6,
                    binder => binder.TransitionTo(QuotesCollected),
                    binder => binder.IfElse(ctx => ctx.Message.NewStatus == 10,
                        b => b.TransitionTo(Cancelled),
                        b => b.Then(_ => { }))));

        During(QuotesCollected,
            When(StatusChanged)
                .Then(ctx => UpdateSagaState(ctx))
                .IfElse(ctx => ctx.Message.NewStatus == 7,
                    binder => binder.TransitionTo(ManagementApproval),
                    binder => binder.IfElse(ctx => ctx.Message.NewStatus == 10,
                        b => b.TransitionTo(Cancelled),
                        b => b.Then(_ => { }))));

        During(ManagementApproval,
            When(StatusChanged)
                .Then(ctx => UpdateSagaState(ctx))
                .IfElse(ctx => ctx.Message.NewStatus == 8,
                    binder => binder.TransitionTo(QuoteApproved),
                    binder => binder.IfElse(ctx => ctx.Message.NewStatus == 4,
                        b => b.TransitionTo(Rejected),
                        b => b.IfElse(ctx => ctx.Message.NewStatus == 10,
                            b2 => b2.TransitionTo(Cancelled),
                            b2 => b2.Then(_ => { })))));

        During(QuoteApproved,
            When(StatusChanged)
                .Then(ctx => UpdateSagaState(ctx))
                .IfElse(ctx => ctx.Message.NewStatus == 9,
                    binder => binder.TransitionTo(Completed),
                    binder => binder.IfElse(ctx => ctx.Message.NewStatus == 10,
                        b => b.TransitionTo(Cancelled),
                        b => b.Then(_ => { }))));

        SetCompletedWhenFinalized();
    }

    private static void UpdateSagaState(BehaviorContext<SalesRequestSagaState, SalesRequestStatusChangedEvent> ctx)
    {
        ctx.Saga.StatusValue = ctx.Message.NewStatus;
        ctx.Saga.UpdatedAt = ctx.Message.OccurredAt;
    }
}
