using MediatR;
using Microsoft.EntityFrameworkCore;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Customers.GetAllCustomers
{
    internal sealed class GetAllCustomersQueryHandler(
        ICustomerRepository customerRepository) : IRequestHandler<GetAllCustomersQuery, Result<List<Customer>>>
    {
        public async Task<Result<List<Customer>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await customerRepository.GetAll().ToListAsync(cancellationToken);
            return customers;
        }
    }
}
