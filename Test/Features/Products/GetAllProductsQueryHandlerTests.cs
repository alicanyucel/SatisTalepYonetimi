using MockQueryable.NSubstitute;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.Products.GetAllProducts;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;

namespace Test.Features.Products;

public class GetAllProductsQueryHandlerTests
{
    private readonly IProductRepository _productRepository = Substitute.For<IProductRepository>();
    private readonly GetAllProductsQueryHandler _handler;

    public GetAllProductsQueryHandlerTests()
    {
        _handler = new GetAllProductsQueryHandler(_productRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new() { Name = "Ürün 1", Code = "P001", UnitPrice = 100m },
            new() { Name = "Ürün 2", Code = "P002", UnitPrice = 200m }
        };
        _productRepository.GetAll().Returns(products.AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetAllProductsQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(2, result.Data!.Count);
    }

    [Fact]
    public async Task Handle_EmptyList_ShouldReturnEmptyResult()
    {
        // Arrange
        _productRepository.GetAll().Returns(new List<Product>().AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetAllProductsQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Data!);
    }
}
