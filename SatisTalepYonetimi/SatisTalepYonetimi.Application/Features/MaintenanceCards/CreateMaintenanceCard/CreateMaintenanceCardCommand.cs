using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.MaintenanceCards.CreateMaintenanceCard
{
    public sealed record CreateMaintenanceCardCommand(
        string Name,
        string Description,
        Guid ProductId,
        int PeriodInDays,
        DateTime LastMaintenanceDate,
        string? AssignedEmail) : IRequest<Result<string>>;
}
