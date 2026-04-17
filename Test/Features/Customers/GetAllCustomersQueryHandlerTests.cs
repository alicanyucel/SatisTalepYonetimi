using MockQueryable.NSubstitute;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.Customers.GetAllCustomers;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;

namespace Test.Features.Customers;

public class GetAllCustomersQueryHandlerTests
{
    private readonly ICustomerRepository _customerRepository = Substitute.For<ICustomerRepository>();
    private readonly GetAllCustomersQueryHandler _handler;

    public GetAllCustomersQueryHandlerTests()
    {
        _handler = new GetAllCustomersQueryHandler(_customerRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllCustomers()
    {
        // Arrange
        var customers = new List<Customer>
        {
            new() { Name = "Müşteri 1", Email = "m1@test.com" },
            new() { Name = "Müşteri 2", Email = "m2@test.com" }
        };
        _customerRepository.GetAll().Returns(customers.AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetAllCustomersQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal(2, result.Data!.Count);
    }

    [Fact]
    public async Task Handle_EmptyList_ShouldReturnEmptyResult()
    {
        // Arrange
        var customers = new List<Customer>();
        _customerRepository.GetAll().Returns(customers.AsQueryable().BuildMock());

        // Act
        var result = await _handler.Handle(new GetAllCustomersQuery(), CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Empty(result.Data!);
    }
}
