using MockQueryable.NSubstitute;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.SalesRequests.GetAllSalesRequests;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;

namespace Test.Features.SalesRequests;

public class GetAllSalesRequestsQueryHandlerTests
{
    private readonly ISalesRequestRepository _salesRequestRepository = Substitute.For<ISalesRequestRepository>();
    private readonly GetAllSalesRequestsQueryHandler _handler;

    public GetAllSalesRequestsQueryHandlerTests()
    {
        _handler = new GetAllSalesRequestsQueryHandler(_salesRequestRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllSalesRequests_OrderedByDateDescending()
    {
        // Arrange
        var salesRequests = new List<SalesRequest>
        {
            new() { RequestNumber = "SR-001", RequestDate = DateTime.UtcNow.AddDays(-2), Customer = new Customer(), RequestedByUser = new AppUser(), Items = [] },
            new() { RequestNumber = "SR-002", RequestDate = DateTime.UtcNow, Customer = new Customer(), RequestedByUser = new AppUser(), Items = [] }
        };
        _salesRequestRepository.GetAll().Returns(salesRequests.AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetAllSalesRequestsQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(2, result.Data!.Count);
        Assert.Equal("SR-002", result.Data![0].RequestNumber);
    }

    [Fact]
    public async Task Handle_EmptyList_ShouldReturnEmptyResult()
    {
        // Arrange
        _salesRequestRepository.GetAll().Returns(new List<SalesRequest>().AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetAllSalesRequestsQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Data!);
    }
}
