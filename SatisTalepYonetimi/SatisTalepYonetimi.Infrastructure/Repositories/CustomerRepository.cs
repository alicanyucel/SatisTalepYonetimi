using GenericRepository;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using SatisTalepYonetimi.Infrastructure.Context;

namespace SatisTalepYonetimi.Infrastructure.Repositories
{
    internal sealed class CustomerRepository : Repository<Customer, ApplicationDbContext>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context) { }
    }
}
