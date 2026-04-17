using MediatR;
using Microsoft.EntityFrameworkCore;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.MaintenanceCards.GetAllMaintenanceCards
{
    internal sealed class GetAllMaintenanceCardsQueryHandler(
        IMaintenanceCardRepository maintenanceCardRepository) : IRequestHandler<GetAllMaintenanceCardsQuery, Result<List<MaintenanceCard>>>
    {
        public async Task<Result<List<MaintenanceCard>>> Handle(GetAllMaintenanceCardsQuery request, CancellationToken cancellationToken)
        {
            var cards = await maintenanceCardRepository.GetAll().Include(p => p.Product).ToListAsync(cancellationToken);
            return cards;
        }
    }
}
