using GenericRepository;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using SatisTalepYonetimi.Infrastructure.Context;

namespace SatisTalepYonetimi.Infrastructure.Repositories
{
    internal sealed class SalesRequestRepository : Repository<SalesRequest, ApplicationDbContext>, ISalesRequestRepository
    {
        public SalesRequestRepository(ApplicationDbContext context) : base(context) { }
    }
}
