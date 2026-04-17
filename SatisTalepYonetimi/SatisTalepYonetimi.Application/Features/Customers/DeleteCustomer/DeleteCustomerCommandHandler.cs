using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Customers.DeleteCustomer
{
    internal sealed class DeleteCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteCustomerCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
            if (customer is null)
                return (500, "Müşteri bulunamadı");

            customerRepository.Delete(customer);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Müşteri başarıyla silindi";
        }
    }
}
