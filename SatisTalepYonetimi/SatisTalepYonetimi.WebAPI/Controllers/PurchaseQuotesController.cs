using MediatR;
using Microsoft.AspNetCore.Mvc;
using SatisTalepYonetimi.Application.Features.PurchaseQuotes.ApproveQuote;
using SatisTalepYonetimi.Application.Features.PurchaseQuotes.CreatePurchaseQuote;
using SatisTalepYonetimi.Application.Features.PurchaseQuotes.GetQuotesBySalesRequest;
using SatisTalepYonetimi.Application.Features.PurchaseQuotes.SubmitQuotesForApproval;
using SatisTalepYonetimi.WebAPI.Abstractions;

namespace SatisTalepYonetimi.WebAPI.Controllers
{
    public sealed class PurchaseQuotesController : ApiController
    {
        public PurchaseQuotesController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePurchaseQuoteCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{salesRequestId}")]
        public async Task<IActionResult> GetBySalesRequest(Guid salesRequestId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetQuotesBySalesRequestQuery(salesRequestId), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForApproval(SubmitQuotesForApprovalCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveQuote(ApproveQuoteCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
