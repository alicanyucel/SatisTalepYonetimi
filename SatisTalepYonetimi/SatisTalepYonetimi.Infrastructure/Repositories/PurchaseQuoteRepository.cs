using GenericRepository;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using SatisTalepYonetimi.Infrastructure.Context;

namespace SatisTalepYonetimi.Infrastructure.Repositories
{
    internal sealed class PurchaseQuoteRepository : Repository<PurchaseQuote, ApplicationDbContext>, IPurchaseQuoteRepository
    {
        public PurchaseQuoteRepository(ApplicationDbContext context) : base(context) { }
    }
}
