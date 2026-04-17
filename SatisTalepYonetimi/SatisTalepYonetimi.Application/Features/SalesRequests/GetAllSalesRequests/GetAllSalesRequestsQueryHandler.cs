using MediatR;
using Microsoft.EntityFrameworkCore;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.SalesRequests.GetAllSalesRequests
{
    internal sealed class GetAllSalesRequestsQueryHandler(
        ISalesRequestRepository salesRequestRepository) : IRequestHandler<GetAllSalesRequestsQuery, Result<List<SalesRequest>>>
    {
        public async Task<Result<List<SalesRequest>>> Handle(GetAllSalesRequestsQuery request, CancellationToken cancellationToken)
        {
            var salesRequests = salesRequestRepository
                .GetAll()
                .Include(p => p.Customer)
                .Include(p => p.RequestedByUser)
                .Include(p => p.Items)
                    .ThenInclude(p => p.Product)
                .OrderByDescending(p => p.RequestDate);

            return await salesRequests.ToListAsync(cancellationToken);
        }
    }
}
