using MediatR;
using Microsoft.AspNetCore.Mvc;
using SatisTalepYonetimi.Application.Features.Products.CreateProduct;
using SatisTalepYonetimi.Application.Features.Products.DeleteProduct;
using SatisTalepYonetimi.Application.Features.Products.GetAllProducts;
using SatisTalepYonetimi.Application.Features.Products.UpdateProduct;
using SatisTalepYonetimi.WebAPI.Abstractions;

namespace SatisTalepYonetimi.WebAPI.Controllers
{
    public sealed class ProductsController : ApiController
    {
        public ProductsController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);
            return StatusCode(response.StatusCode, response);
        }
    }
}
