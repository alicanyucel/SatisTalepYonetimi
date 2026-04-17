using MediatR;
using Microsoft.AspNetCore.Mvc;
using SatisTalepYonetimi.Application.Features.MaintenanceTickets.GetAllMaintenanceTickets;
using SatisTalepYonetimi.Application.Features.MaintenanceTickets.UpdateMaintenanceTicketStatus;
using SatisTalepYonetimi.WebAPI.Abstractions;

namespace SatisTalepYonetimi.WebAPI.Controllers
{
    public sealed class MaintenanceTicketsController : ApiController
    {
        public MaintenanceTicketsController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllMaintenanceTicketsQuery(), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStatus(UpdateMaintenanceTicketStatusCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
