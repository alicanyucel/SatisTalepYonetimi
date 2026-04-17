using MediatR;
using SatisTalepYonetimi.Domain.Entities;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.SalesRequests.GetAllSalesRequests
{
    public sealed record GetAllSalesRequestsQuery() : IRequest<Result<List<SalesRequest>>>;
}
