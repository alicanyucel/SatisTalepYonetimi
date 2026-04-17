using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.MaintenanceCards.DeleteMaintenanceCard
{
    public sealed record DeleteMaintenanceCardCommand(Guid Id) : IRequest<Result<string>>;
}
