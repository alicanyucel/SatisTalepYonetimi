using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.MaintenanceCards.DeleteMaintenanceCard
{
    internal sealed class DeleteMaintenanceCardCommandHandler(
        IMaintenanceCardRepository maintenanceCardRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteMaintenanceCardCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteMaintenanceCardCommand request, CancellationToken cancellationToken)
        {
            var card = await maintenanceCardRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
            if (card is null)
                return (500, "Bakım kartı bulunamadı");

            maintenanceCardRepository.Delete(card);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Bakım kartı başarıyla silindi";
        }
    }
}
