using MediatR;
using Microsoft.EntityFrameworkCore;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Suppliers.GetAllSuppliers
{
    internal sealed class GetAllSuppliersQueryHandler(
        ISupplierRepository supplierRepository) : IRequestHandler<GetAllSuppliersQuery, Result<List<Supplier>>>
    {
        public async Task<Result<List<Supplier>>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await supplierRepository.GetAll().ToListAsync(cancellationToken);
            return suppliers;
        }
    }
}
