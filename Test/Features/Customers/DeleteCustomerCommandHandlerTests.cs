using GenericRepository;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.Customers.DeleteCustomer;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using System.Linq.Expressions;

namespace Test.Features.Customers;

public class DeleteCustomerCommandHandlerTests
{
    private readonly ICustomerRepository _customerRepository = Substitute.For<ICustomerRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly DeleteCustomerCommandHandler _handler;

    public DeleteCustomerCommandHandlerTests()
    {
        _handler = new DeleteCustomerCommandHandler(_customerRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_CustomerExists_ShouldDeleteAndReturnSuccess()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var customer = new Customer { Id = customerId, Name = "Test" };
        _customerRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Customer, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(customer);

        var command = new DeleteCustomerCommand(customerId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("Müşteri başarıyla silindi", result.Data);
        _customerRepository.Received(1).Delete(customer);
    }

    [Fact]
    public async Task Handle_CustomerNotFound_ShouldReturnError()
    {
        // Arrange
        _customerRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Customer, bool>>>(), Arg.Any<CancellationToken>())
            .Returns((Customer?)null);

        var command = new DeleteCustomerCommand(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
        _customerRepository.DidNotReceive().Delete(Arg.Any<Customer>());
    }
}
