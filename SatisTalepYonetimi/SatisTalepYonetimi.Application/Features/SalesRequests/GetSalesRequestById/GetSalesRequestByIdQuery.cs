using MediatR;
using SatisTalepYonetimi.Domain.Entities;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.SalesRequests.GetSalesRequestById
{
    public sealed record GetSalesRequestByIdQuery(Guid Id) : IRequest<Result<SalesRequest>>;
}
