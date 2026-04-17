using GenericRepository;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using SatisTalepYonetimi.Infrastructure.Context;

namespace SatisTalepYonetimi.Infrastructure.Repositories
{
    internal sealed class MaintenanceCardRepository : Repository<MaintenanceCard, ApplicationDbContext>, IMaintenanceCardRepository
    {
        public MaintenanceCardRepository(ApplicationDbContext context) : base(context) { }
    }
}
