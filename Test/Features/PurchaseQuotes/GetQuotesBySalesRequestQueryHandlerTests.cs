using MockQueryable.NSubstitute;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.PurchaseQuotes.GetQuotesBySalesRequest;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;

namespace Test.Features.PurchaseQuotes;

public class GetQuotesBySalesRequestQueryHandlerTests
{
    private readonly IPurchaseQuoteRepository _purchaseQuoteRepository = Substitute.For<IPurchaseQuoteRepository>();
    private readonly GetQuotesBySalesRequestQueryHandler _handler;

    public GetQuotesBySalesRequestQueryHandlerTests()
    {
        _handler = new GetQuotesBySalesRequestQueryHandler(_purchaseQuoteRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnQuotesForGivenSalesRequest()
    {
        // Arrange
        var salesRequestId = Guid.NewGuid();
        var quotes = new List<PurchaseQuote>
        {
            new() { SalesRequestId = salesRequestId, TotalAmount = 1000m, Supplier = new Supplier { Name = "Tedarikçi 1" } },
            new() { SalesRequestId = salesRequestId, TotalAmount = 1500m, Supplier = new Supplier { Name = "Tedarikçi 2" } },
            new() { SalesRequestId = Guid.NewGuid(), TotalAmount = 2000m, Supplier = new Supplier { Name = "Tedarikçi 3" } }
        };
        _purchaseQuoteRepository.GetAll().Returns(quotes.AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetQuotesBySalesRequestQuery(salesRequestId), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(2, result.Data!.Count);
        Assert.All(result.Data, q => Assert.Equal(salesRequestId, q.SalesRequestId));
    }

    [Fact]
    public async Task Handle_NoQuotesFound_ShouldReturnEmptyList()
    {
        // Arrange
        _purchaseQuoteRepository.GetAll().Returns(new List<PurchaseQuote>().AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetQuotesBySalesRequestQuery(Guid.NewGuid()), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Data!);
    }
}
