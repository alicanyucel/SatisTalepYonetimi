using AutoMapper;
using GenericRepository;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.Customers.UpdateCustomer;
using SatisTalepYonetimi.Application.Mapping;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using System.Linq.Expressions;

namespace Test.Features.Customers;

public class UpdateCustomerCommandHandlerTests
{
    private readonly ICustomerRepository _customerRepository = Substitute.For<ICustomerRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IMapper _mapper;
    private readonly UpdateCustomerCommandHandler _handler;

    public UpdateCustomerCommandHandlerTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
        _handler = new UpdateCustomerCommandHandler(_customerRepository, _unitOfWork, _mapper);
    }

    [Fact]
    public async Task Handle_CustomerExists_ShouldUpdateAndReturnSuccess()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var customer = new Customer { Id = customerId, Name = "Eski Ad" };
        _customerRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Customer, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(customer);

        var command = new UpdateCustomerCommand(customerId, "Yeni Ad", "yeni@test.com", "5559999999", "Ankara", "9999999999", true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("Müşteri başarıyla güncellendi", result.Data);
        _customerRepository.Received(1).Update(customer);
    }

    [Fact]
    public async Task Handle_CustomerNotFound_ShouldReturnError()
    {
        // Arrange
        _customerRepository.GetByExpressionAsync(Arg.Any<Expression<Func<Customer, bool>>>(), Arg.Any<CancellationToken>())
            .Returns((Customer?)null);

        var command = new UpdateCustomerCommand(Guid.NewGuid(), "Ad", "e@e.com", "555", "Adres", "123", true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccessful);
    }
}
