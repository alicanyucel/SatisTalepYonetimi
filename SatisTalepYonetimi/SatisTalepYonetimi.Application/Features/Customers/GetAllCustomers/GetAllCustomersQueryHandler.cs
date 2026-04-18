using MediatR;
using Microsoft.EntityFrameworkCore;
using SatisTalepYonetimi.Application.Services;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Customers.GetAllCustomers
{
    internal sealed class GetAllCustomersQueryHandler(
        ICustomerRepository customerRepository,
        ICacheService cacheService) : IRequestHandler<GetAllCustomersQuery, Result<List<Customer>>>
    {
        private const string CacheKey = "customers:all";

        public async Task<Result<List<Customer>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var cached = await cacheService.GetAsync<List<Customer>>(CacheKey, cancellationToken);
            if (cached is not null)
                return cached;

            var customers = await customerRepository.GetAll().ToListAsync(cancellationToken);

            await cacheService.SetAsync(CacheKey, customers, TimeSpan.FromMinutes(5), cancellationToken);

            return customers;
        }
    }
}
