using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.SalesRequests.UpdateSalesRequestStatus
{
    public sealed record UpdateSalesRequestStatusCommand(
        Guid Id,
        int StatusValue) : IRequest<Result<string>>;
}
