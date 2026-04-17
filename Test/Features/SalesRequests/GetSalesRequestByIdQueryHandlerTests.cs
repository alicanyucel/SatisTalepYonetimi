using MockQueryable.NSubstitute;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.SalesRequests.GetSalesRequestById;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;

namespace Test.Features.SalesRequests;

public class GetSalesRequestByIdQueryHandlerTests
{
    private readonly ISalesRequestRepository _salesRequestRepository = Substitute.For<ISalesRequestRepository>();
    private readonly GetSalesRequestByIdQueryHandler _handler;

    public GetSalesRequestByIdQueryHandlerTests()
    {
        _handler = new GetSalesRequestByIdQueryHandler(_salesRequestRepository);
    }

    [Fact]
    public async Task Handle_SalesRequestExists_ShouldReturnSalesRequest()
    {
        // Arrange
        var id = Guid.NewGuid();
        var salesRequests = new List<SalesRequest>
        {
            new() { Id = id, RequestNumber = "SR-001", Customer = new Customer(), RequestedByUser = new AppUser(), Items = [] }
        };
        _salesRequestRepository.GetAll().Returns(salesRequests.AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetSalesRequestByIdQuery(id), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("SR-001", result.Data!.RequestNumber);
    }

    [Fact]
    public async Task Handle_SalesRequestNotFound_ShouldReturnError()
    {
        // Arrange
        _salesRequestRepository.GetAll().Returns(new List<SalesRequest>().AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetSalesRequestByIdQuery(Guid.NewGuid()), CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
    }
}
