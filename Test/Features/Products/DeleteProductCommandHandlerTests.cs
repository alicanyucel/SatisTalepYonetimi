using GenericRepository;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.Products.DeleteProduct;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using System.Linq.Expressions;

namespace Test.Features.Products;

public class DeleteProductCommandHandlerTests
{
    private readonly IProductRepository _productRepository = Substitute.For<IProductRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly DeleteProductCommandHandler _handler;

    public DeleteProductCommandHandlerTests()
    {
        _handler = new DeleteProductCommandHandler(_productRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ProductExists_ShouldDeleteAndReturnSuccess()
    {
        // Arrange
        var product = new Product { Id = Guid.NewGuid(), Name = "Test" };
        _productRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(product);

        var command = new DeleteProductCommand(product.Id);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("Ürün başarıyla silindi", result.Data);
        _productRepository.Received(1).Delete(product);
    }

    [Fact]
    public async Task Handle_ProductNotFound_ShouldReturnError()
    {
        // Arrange
        _productRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>())
            .Returns((Product?)null);

        var command = new DeleteProductCommand(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
    }
}
