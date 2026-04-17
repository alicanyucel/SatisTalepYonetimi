using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;

namespace SatisTalepYonetimi.Application.Services
{
    public interface IMaintenanceCheckService
    {
        Task CheckAndCreateTicketsAsync();
    }

    internal sealed class MaintenanceCheckService(
        IMaintenanceCardRepository maintenanceCardRepository,
        IMaintenanceTicketRepository maintenanceTicketRepository,
        IUnitOfWork unitOfWork,
        ILogger<MaintenanceCheckService> logger) : IMaintenanceCheckService
    {
        public async Task CheckAndCreateTicketsAsync()
        {
            var cards = await maintenanceCardRepository.GetAll().Where(c => c.IsActive && c.NextMaintenanceDate.Date <= DateTime.UtcNow.Date).ToListAsync(default);
            foreach (var card in cards)
            {
                var ticket = new MaintenanceTicket
                {
                    MaintenanceCardId = card.Id,
                    TicketNumber = $"MT-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString()[..4].ToUpper()}",
                    Description = $"Periyodik bakım: {card.Name}",
                    StatusValue = 1,
                    CreatedAt = DateTime.UtcNow
                };

                await maintenanceTicketRepository.AddAsync(ticket, default);

                if (!string.IsNullOrWhiteSpace(card.AssignedEmail))
                {
                    logger.LogInformation(
                        "Bakım uyarı maili gönderilecek: {Email} - Bakım kartı: {CardName} - Ürün ID: {ProductId}",
                        card.AssignedEmail, card.Name, card.ProductId);
                }

                logger.LogInformation("Bakım fişi oluşturuldu: {TicketNumber} - Kart: {CardName}", ticket.TicketNumber, card.Name);
            }

            await unitOfWork.SaveChangesAsync(default);
        }
    }
}
