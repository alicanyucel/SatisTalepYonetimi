using AutoMapper;
using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Customers.CreateCustomer
{
    internal sealed class CreateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateCustomerCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = mapper.Map<Customer>(request);
            await customerRepository.AddAsync(customer, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Müşteri başarıyla oluşturuldu";
        }
    }
}
