using MockQueryable.NSubstitute;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.Suppliers.GetAllSuppliers;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;

namespace Test.Features.Suppliers;

public class GetAllSuppliersQueryHandlerTests
{
    private readonly ISupplierRepository _supplierRepository = Substitute.For<ISupplierRepository>();
    private readonly GetAllSuppliersQueryHandler _handler;

    public GetAllSuppliersQueryHandlerTests()
    {
        _handler = new GetAllSuppliersQueryHandler(_supplierRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllSuppliers()
    {
        // Arrange
        var suppliers = new List<Supplier>
        {
            new() { Name = "Tedarikçi 1", Email = "t1@test.com" },
            new() { Name = "Tedarikçi 2", Email = "t2@test.com" }
        };
        _supplierRepository.GetAll().Returns(suppliers.AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetAllSuppliersQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(2, result.Data!.Count);
    }

    [Fact]
    public async Task Handle_EmptyList_ShouldReturnEmptyResult()
    {
        // Arrange
        _supplierRepository.GetAll().Returns(new List<Supplier>().AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetAllSuppliersQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Data!);
    }
}
