using MediatR;
using Microsoft.EntityFrameworkCore;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.MaintenanceTickets.GetAllMaintenanceTickets
{
    internal sealed class GetAllMaintenanceTicketsQueryHandler(
        IMaintenanceTicketRepository maintenanceTicketRepository) : IRequestHandler<GetAllMaintenanceTicketsQuery, Result<List<MaintenanceTicket>>>
    {
        public async Task<Result<List<MaintenanceTicket>>> Handle(GetAllMaintenanceTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = await maintenanceTicketRepository.GetAll().Include(p => p.MaintenanceCard).ToListAsync(cancellationToken);
            return tickets;
        }
    }
}
