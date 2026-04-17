using MediatR;
using SatisTalepYonetimi.Domain.Entities;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.MaintenanceCards.GetAllMaintenanceCards
{
    public sealed record GetAllMaintenanceCardsQuery() : IRequest<Result<List<MaintenanceCard>>>;
}
