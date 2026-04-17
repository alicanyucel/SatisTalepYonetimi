using GenericRepository;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.SalesRequests.CreateSalesRequest;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using System.Linq.Expressions;

namespace Test.Features.SalesRequests;

public class CreateSalesRequestCommandHandlerTests
{
    private readonly ISalesRequestRepository _salesRequestRepository = Substitute.For<ISalesRequestRepository>();
    private readonly IProductRepository _productRepository = Substitute.For<IProductRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly CreateSalesRequestCommandHandler _handler;

    public CreateSalesRequestCommandHandlerTests()
    {
        _handler = new CreateSalesRequestCommandHandler(_salesRequestRepository, _productRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ValidItems_ShouldCreateSalesRequestWithCorrectTotalAmount()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product { Id = productId, Name = "Ürün", UnitPrice = 50m };

        _productRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(product);

        var command = new CreateSalesRequestCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Test notu",
            [new CreateSalesRequestItemDto(productId, 3)]);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        await _salesRequestRepository.Received(1).AddAsync(
            Arg.Is<SalesRequest>(sr => sr.TotalAmount == 150m && sr.Items.Count == 1),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ProductNotFound_ShouldReturnError()
    {
        // Arrange
        _productRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>())
            .Returns((Product?)null);

        var command = new CreateSalesRequestCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            null,
            [new CreateSalesRequestItemDto(Guid.NewGuid(), 1)]);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        await _salesRequestRepository.DidNotReceive().AddAsync(Arg.Any<SalesRequest>(), Arg.Any<CancellationToken>());
    }
}
