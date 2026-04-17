using MediatR;
using SatisTalepYonetimi.Domain.Entities;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.MaintenanceTickets.GetAllMaintenanceTickets
{
    public sealed record GetAllMaintenanceTicketsQuery() : IRequest<Result<List<MaintenanceTicket>>>;
}
