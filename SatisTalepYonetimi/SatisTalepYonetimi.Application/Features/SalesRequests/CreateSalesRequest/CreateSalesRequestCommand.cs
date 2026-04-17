using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.SalesRequests.CreateSalesRequest
{
    public sealed record CreateSalesRequestCommand(
        Guid CustomerId,
        Guid RequestedByUserId,
        string? Note,
        List<CreateSalesRequestItemDto> Items) : IRequest<Result<string>>;
}
