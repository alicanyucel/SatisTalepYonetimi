using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.SalesRequests.DeleteSalesRequest
{
    public sealed record DeleteSalesRequestCommand(Guid Id) : IRequest<Result<string>>;
}
