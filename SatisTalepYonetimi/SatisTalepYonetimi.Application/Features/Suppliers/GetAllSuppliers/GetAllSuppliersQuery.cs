using MediatR;
using SatisTalepYonetimi.Domain.Entities;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.Suppliers.GetAllSuppliers
{
    public sealed record GetAllSuppliersQuery() : IRequest<Result<List<Supplier>>>;
}
