using GenericRepository;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using SatisTalepYonetimi.Infrastructure.Context;

namespace SatisTalepYonetimi.Infrastructure.Repositories
{
    internal sealed class MaintenanceTicketRepository : Repository<MaintenanceTicket, ApplicationDbContext>, IMaintenanceTicketRepository
    {
        public MaintenanceTicketRepository(ApplicationDbContext context) : base(context) { }
    }
}
