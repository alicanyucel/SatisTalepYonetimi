using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Enums;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.MaintenanceTickets.UpdateMaintenanceTicketStatus
{
    internal sealed class UpdateMaintenanceTicketStatusCommandHandler(
        IMaintenanceTicketRepository maintenanceTicketRepository,
        IMaintenanceCardRepository maintenanceCardRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateMaintenanceTicketStatusCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateMaintenanceTicketStatusCommand request, CancellationToken cancellationToken)
        {
            var ticket = await maintenanceTicketRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
            if (ticket is null)
                return (500, "Bakım fişi bulunamadı");

            if (!MaintenanceStatusEnum.TryFromValue(request.StatusValue, out _))
                return (500, "Geçersiz durum değeri");

            ticket.StatusValue = request.StatusValue;

            if (request.StatusValue == MaintenanceStatusEnum.Completed.Value)
            {
                ticket.CompletedAt = DateTime.UtcNow;

                var card = await maintenanceCardRepository.GetByExpressionAsync(p => p.Id == ticket.MaintenanceCardId, cancellationToken);
                if (card is not null)
                {
                    card.LastMaintenanceDate = DateTime.UtcNow;
                    card.NextMaintenanceDate = DateTime.UtcNow.AddDays(card.PeriodInDays);
                    maintenanceCardRepository.Update(card);
                }
            }

            maintenanceTicketRepository.Update(ticket);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var status = MaintenanceStatusEnum.FromValue(request.StatusValue);
            return $"Bakım fişi durumu '{status.Name}' olarak güncellendi";
        }
    }
}
