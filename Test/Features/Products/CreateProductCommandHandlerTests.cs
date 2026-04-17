using AutoMapper;
using GenericRepository;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.Products.CreateProduct;
using SatisTalepYonetimi.Application.Mapping;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using System.Linq.Expressions;

namespace Test.Features.Products;

public class CreateProductCommandHandlerTests
{
    private readonly IProductRepository _productRepository = Substitute.For<IProductRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IMapper _mapper;
    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandHandlerTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
        _handler = new CreateProductCommandHandler(_productRepository, _unitOfWork, _mapper);
    }

    [Fact]
    public async Task Handle_NewProduct_ShouldCreateAndReturnSuccess()
    {
        // Arrange
        _productRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>())
            .Returns((Product?)null);

        var command = new CreateProductCommand("Ürün A", "PRD-001", "Açıklama", 100m, "Adet", 50);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("Ürün başarıyla oluşturuldu", result.Data);
        await _productRepository.Received(1).AddAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_DuplicateCode_ShouldReturnError()
    {
        // Arrange
        _productRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Product, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(new Product { Code = "PRD-001" });

        var command = new CreateProductCommand("Ürün A", "PRD-001", "Açıklama", 100m, "Adet", 50);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        await _productRepository.DidNotReceive().AddAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
    }
}
