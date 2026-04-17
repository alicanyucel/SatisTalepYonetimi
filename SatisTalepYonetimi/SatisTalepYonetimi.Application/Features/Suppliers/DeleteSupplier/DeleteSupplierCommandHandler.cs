using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Suppliers.DeleteSupplier
{
    internal sealed class DeleteSupplierCommandHandler(
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteSupplierCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await supplierRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
            if (supplier is null)
                return (500, "Tedarikçi bulunamadı");

            supplierRepository.Delete(supplier);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Tedarikçi başarıyla silindi";
        }
    }
}
