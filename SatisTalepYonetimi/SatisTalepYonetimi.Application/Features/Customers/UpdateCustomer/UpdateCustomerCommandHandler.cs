using AutoMapper;
using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Customers.UpdateCustomer
{
    internal sealed class UpdateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdateCustomerCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
            if (customer is null)
                return (500, "Müşteri bulunamadı");

            mapper.Map(request, customer);
            customerRepository.Update(customer);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Müşteri başarıyla güncellendi";
        }
    }
}
