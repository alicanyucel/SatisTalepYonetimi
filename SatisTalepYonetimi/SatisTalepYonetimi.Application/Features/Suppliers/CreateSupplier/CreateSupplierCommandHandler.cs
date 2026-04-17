using AutoMapper;
using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Suppliers.CreateSupplier
{
    internal sealed class CreateSupplierCommandHandler(
        ISupplierRepository supplierRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateSupplierCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = mapper.Map<Supplier>(request);
            await supplierRepository.AddAsync(supplier, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Tedarikçi başarıyla oluşturuldu";
        }
    }
}
