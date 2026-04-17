using GenericRepository;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using SatisTalepYonetimi.Infrastructure.Context;

namespace SatisTalepYonetimi.Infrastructure.Repositories
{
    internal sealed class ProductRepository : Repository<Product, ApplicationDbContext>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }
    }
}
