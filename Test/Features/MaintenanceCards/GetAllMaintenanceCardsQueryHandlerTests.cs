using MockQueryable.NSubstitute;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.MaintenanceCards.GetAllMaintenanceCards;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;

namespace Test.Features.MaintenanceCards;

public class GetAllMaintenanceCardsQueryHandlerTests
{
    private readonly IMaintenanceCardRepository _maintenanceCardRepository = Substitute.For<IMaintenanceCardRepository>();
    private readonly GetAllMaintenanceCardsQueryHandler _handler;

    public GetAllMaintenanceCardsQueryHandlerTests()
    {
        _handler = new GetAllMaintenanceCardsQueryHandler(_maintenanceCardRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllMaintenanceCards()
    {
        // Arrange
        var cards = new List<MaintenanceCard>
        {
            new() { Name = "Bakım 1", Product = new Product { Name = "Ürün 1" }, PeriodInDays = 30 },
            new() { Name = "Bakım 2", Product = new Product { Name = "Ürün 2" }, PeriodInDays = 60 }
        };
        _maintenanceCardRepository.GetAll().Returns(cards.AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetAllMaintenanceCardsQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(2, result.Data!.Count);
    }

    [Fact]
    public async Task Handle_EmptyList_ShouldReturnEmptyResult()
    {
        // Arrange
        _maintenanceCardRepository.GetAll().Returns(new List<MaintenanceCard>().AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetAllMaintenanceCardsQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Data!);
    }
}
