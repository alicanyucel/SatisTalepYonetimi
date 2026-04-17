using GenericRepository;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.SalesRequests.DeleteSalesRequest;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using System.Linq.Expressions;

namespace Test.Features.SalesRequests;

public class DeleteSalesRequestCommandHandlerTests
{
    private readonly ISalesRequestRepository _salesRequestRepository = Substitute.For<ISalesRequestRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly DeleteSalesRequestCommandHandler _handler;

    public DeleteSalesRequestCommandHandlerTests()
    {
        _handler = new DeleteSalesRequestCommandHandler(_salesRequestRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_SalesRequestExists_ShouldDeleteAndReturnSuccess()
    {
        // Arrange
        var salesRequest = new SalesRequest { Id = Guid.NewGuid() };
        _salesRequestRepository.GetByExpressionAsync(Arg.Any<Expression<Func<SalesRequest, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(salesRequest);

        var command = new DeleteSalesRequestCommand(salesRequest.Id);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("Satış talebi başarıyla silindi", result.Data);
        _salesRequestRepository.Received(1).Delete(salesRequest);
    }

    [Fact]
    public async Task Handle_SalesRequestNotFound_ShouldReturnError()
    {
        // Arrange
        _salesRequestRepository.GetByExpressionAsync(Arg.Any<Expression<Func<SalesRequest, bool>>>(), Arg.Any<CancellationToken>())
            .Returns((SalesRequest?)null);

        var command = new DeleteSalesRequestCommand(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        _salesRequestRepository.DidNotReceive().Delete(Arg.Any<SalesRequest>());
    }
}
