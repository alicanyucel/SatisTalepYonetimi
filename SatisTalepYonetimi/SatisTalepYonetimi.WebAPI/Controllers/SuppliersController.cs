using MediatR;
using Microsoft.AspNetCore.Mvc;
using SatisTalepYonetimi.Application.Features.Suppliers.CreateSupplier;
using SatisTalepYonetimi.Application.Features.Suppliers.DeleteSupplier;
using SatisTalepYonetimi.Application.Features.Suppliers.GetAllSuppliers;
using SatisTalepYonetimi.WebAPI.Abstractions;

namespace SatisTalepYonetimi.WebAPI.Controllers
{
    [Route("api/TedarikciTanimlari/[action]")]
    public sealed class SuppliersController : ApiController
    {
        public SuppliersController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteSupplierCommand(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllSuppliersQuery(), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
