using MediatR;
using Microsoft.EntityFrameworkCore;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.SalesRequests.GetSalesRequestById
{
    internal sealed class GetSalesRequestByIdQueryHandler(
        ISalesRequestRepository salesRequestRepository) : IRequestHandler<GetSalesRequestByIdQuery, Result<SalesRequest>>
    {
        public async Task<Result<SalesRequest>> Handle(GetSalesRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var salesRequest = await salesRequestRepository
                .GetAll()
                .Include(p => p.Customer)
                .Include(p => p.RequestedByUser)
                .Include(p => p.Items)
                    .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (salesRequest is null)
                return (500, "Satış talebi bulunamadı");

            return salesRequest;
        }
    }
}
