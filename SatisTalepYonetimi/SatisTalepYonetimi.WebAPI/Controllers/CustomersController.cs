using MediatR;
using Microsoft.AspNetCore.Mvc;
using SatisTalepYonetimi.Application.Features.Customers.CreateCustomer;
using SatisTalepYonetimi.Application.Features.Customers.DeleteCustomer;
using SatisTalepYonetimi.Application.Features.Customers.GetAllCustomers;
using SatisTalepYonetimi.Application.Features.Customers.UpdateCustomer;
using SatisTalepYonetimi.WebAPI.Abstractions;

namespace SatisTalepYonetimi.WebAPI.Controllers
{
    public sealed class CustomersController : ApiController
    {
        public CustomersController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteCustomerCommand(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllCustomersQuery(), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
