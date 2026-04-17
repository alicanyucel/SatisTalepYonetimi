using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.MaintenanceTickets.UpdateMaintenanceTicketStatus
{
    public sealed record UpdateMaintenanceTicketStatusCommand(
        Guid Id,
        int StatusValue) : IRequest<Result<string>>;
}
