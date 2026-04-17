using GenericRepository;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using SatisTalepYonetimi.Infrastructure.Context;

namespace SatisTalepYonetimi.Infrastructure.Repositories
{
    internal sealed class SalesRequestItemRepository : Repository<SalesRequestItem, ApplicationDbContext>, ISalesRequestItemRepository
    {
        public SalesRequestItemRepository(ApplicationDbContext context) : base(context) { }
    }
}
