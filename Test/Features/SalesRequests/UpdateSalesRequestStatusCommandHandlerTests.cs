using GenericRepository;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.SalesRequests.UpdateSalesRequestStatus;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using System.Linq.Expressions;

namespace Test.Features.SalesRequests;

public class UpdateSalesRequestStatusCommandHandlerTests
{
    private readonly ISalesRequestRepository _salesRequestRepository = Substitute.For<ISalesRequestRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly UpdateSalesRequestStatusCommandHandler _handler;

    public UpdateSalesRequestStatusCommandHandlerTests()
    {
        _handler = new UpdateSalesRequestStatusCommandHandler(_salesRequestRepository, _unitOfWork);
    }

    [Theory]
    [InlineData(1, 2)]  // Beklemede → Yönetici Onayında
    [InlineData(2, 3)]  // Yönetici Onayında → Yönetici Onayladı
    [InlineData(2, 4)]  // Yönetici Onayında → Reddedildi
    [InlineData(3, 5)]  // Yönetici Onayladı → Satınalma Sürecinde
    [InlineData(8, 9)]  // Teklif Onaylandı → Tamamlandı
    [InlineData(5, 10)] // Herhangi bir durum → İptal Edildi
    public async Task Handle_ValidTransition_ShouldUpdateStatusAndReturnSuccess(int currentStatus, int newStatus)
    {
        // Arrange
        var salesRequest = new SalesRequest { Id = Guid.NewGuid(), StatusValue = currentStatus };
        _salesRequestRepository.GetByExpressionAsync(Arg.Any<Expression<Func<SalesRequest, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(salesRequest);

        var command = new UpdateSalesRequestStatusCommand(salesRequest.Id, newStatus);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(newStatus, salesRequest.StatusValue);
        _salesRequestRepository.Received(1).Update(salesRequest);
    }

    [Theory]
    [InlineData(1, 3)]  // Beklemede → Yönetici Onayladı (geçersiz)
    [InlineData(1, 5)]  // Beklemede → Satınalma Sürecinde (geçersiz)
    [InlineData(4, 5)]  // Reddedildi → Satınalma Sürecinde (geçersiz)
    [InlineData(9, 1)]  // Tamamlandı → Beklemede (geçersiz)
    public async Task Handle_InvalidTransition_ShouldReturnError(int currentStatus, int newStatus)
    {
        // Arrange
        var salesRequest = new SalesRequest { Id = Guid.NewGuid(), StatusValue = currentStatus };
        _salesRequestRepository.GetByExpressionAsync(Arg.Any<Expression<Func<SalesRequest, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(salesRequest);

        var command = new UpdateSalesRequestStatusCommand(salesRequest.Id, newStatus);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        _salesRequestRepository.DidNotReceive().Update(Arg.Any<SalesRequest>());
    }

    [Fact]
    public async Task Handle_SalesRequestNotFound_ShouldReturnError()
    {
        // Arrange
        _salesRequestRepository.GetByExpressionAsync(Arg.Any<Expression<Func<SalesRequest, bool>>>(), Arg.Any<CancellationToken>())
            .Returns((SalesRequest?)null);

        var command = new UpdateSalesRequestStatusCommand(Guid.NewGuid(), 2);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
    }

    [Fact]
    public async Task Handle_TransitionToManagerApproved_ShouldSetApprovedDate()
    {
        // Arrange
        var salesRequest = new SalesRequest { Id = Guid.NewGuid(), StatusValue = 2 };
        _salesRequestRepository.GetByExpressionAsync(Arg.Any<Expression<Func<SalesRequest, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(salesRequest);

        var command = new UpdateSalesRequestStatusCommand(salesRequest.Id, 3);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.NotNull(salesRequest.ApprovedDate);
    }
}
