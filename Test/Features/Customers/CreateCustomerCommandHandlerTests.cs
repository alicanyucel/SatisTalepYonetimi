using AutoMapper;
using GenericRepository;
using NSubstitute;
using SatisTalepYonetimi.Application.Features.Customers.CreateCustomer;
using SatisTalepYonetimi.Application.Mapping;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;

namespace Test.Features.Customers;

public class CreateCustomerCommandHandlerTests
{
    private readonly ICustomerRepository _customerRepository = Substitute.For<ICustomerRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IMapper _mapper;
    private readonly CreateCustomerCommandHandler _handler;

    public CreateCustomerCommandHandlerTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
        _handler = new CreateCustomerCommandHandler(_customerRepository, _unitOfWork, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldCreateCustomer_AndReturnSuccessMessage()
    {
        // Arrange
        var command = new CreateCustomerCommand("Test Müşteri", "test@test.com", "5551234567", "İstanbul", "1234567890");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccessful);
        Assert.Equal("Müşteri başarıyla oluşturuldu", result.Data);
        await _customerRepository.Received(1).AddAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
