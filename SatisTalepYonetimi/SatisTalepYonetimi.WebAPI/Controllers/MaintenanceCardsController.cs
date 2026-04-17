using MediatR;
using Microsoft.AspNetCore.Mvc;
using SatisTalepYonetimi.Application.Features.MaintenanceCards.CreateMaintenanceCard;
using SatisTalepYonetimi.Application.Features.MaintenanceCards.DeleteMaintenanceCard;
using SatisTalepYonetimi.Application.Features.MaintenanceCards.GetAllMaintenanceCards;
using SatisTalepYonetimi.WebAPI.Abstractions;

namespace SatisTalepYonetimi.WebAPI.Controllers
{
    public sealed class MaintenanceCardsController : ApiController
    {
        public MaintenanceCardsController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMaintenanceCardCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteMaintenanceCardCommand(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllMaintenanceCardsQuery(), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
