using MediatR;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.PurchaseQuotes.SubmitQuotesForApproval
{
    public sealed record SubmitQuotesForApprovalCommand(Guid SalesRequestId) : IRequest<Result<string>>;
}
