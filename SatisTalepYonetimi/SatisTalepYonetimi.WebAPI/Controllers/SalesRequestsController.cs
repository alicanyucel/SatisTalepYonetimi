using MediatR;
using Microsoft.AspNetCore.Mvc;
using SatisTalepYonetimi.Application.Features.SalesRequests.CreateSalesRequest;
using SatisTalepYonetimi.Application.Features.SalesRequests.DeleteSalesRequest;
using SatisTalepYonetimi.Application.Features.SalesRequests.GetAllSalesRequests;
using SatisTalepYonetimi.Application.Features.SalesRequests.GetSalesRequestById;
using SatisTalepYonetimi.Application.Features.SalesRequests.UpdateSalesRequestStatus;
using SatisTalepYonetimi.WebAPI.Abstractions;

namespace SatisTalepYonetimi.WebAPI.Controllers
{
    public sealed class SalesRequestsController : ApiController
    {
        public SalesRequestsController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSalesRequestCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStatus(UpdateSalesRequestStatusCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteSalesRequestCommand(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllSalesRequestsQuery(), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetSalesRequestByIdQuery(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
