using MockQueryable.NSubstitute;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.MaintenanceTickets.GetAllMaintenanceTickets;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;

namespace Test.Features.MaintenanceTickets;

public class GetAllMaintenanceTicketsQueryHandlerTests
{
    private readonly IMaintenanceTicketRepository _maintenanceTicketRepository = Substitute.For<IMaintenanceTicketRepository>();
    private readonly GetAllMaintenanceTicketsQueryHandler _handler;

    public GetAllMaintenanceTicketsQueryHandlerTests()
    {
        _handler = new GetAllMaintenanceTicketsQueryHandler(_maintenanceTicketRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllMaintenanceTickets()
    {
        // Arrange
        var tickets = new List<MaintenanceTicket>
        {
            new() { TicketNumber = "MT-001", MaintenanceCard = new MaintenanceCard { Name = "Bakım 1", Product = new Product() } },
            new() { TicketNumber = "MT-002", MaintenanceCard = new MaintenanceCard { Name = "Bakım 2", Product = new Product() } }
        };
        _maintenanceTicketRepository.GetAll().Returns(tickets.AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetAllMaintenanceTicketsQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(2, result.Data!.Count);
    }

    [Fact]
    public async Task Handle_EmptyList_ShouldReturnEmptyResult()
    {
        // Arrange
        _maintenanceTicketRepository.GetAll().Returns(new List<MaintenanceTicket>().AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetAllMaintenanceTicketsQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Data!);
    }
}
