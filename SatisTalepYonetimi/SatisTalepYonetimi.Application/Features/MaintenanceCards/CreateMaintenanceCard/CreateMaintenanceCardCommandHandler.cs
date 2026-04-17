using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Entities;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.MaintenanceCards.CreateMaintenanceCard
{
    internal sealed class CreateMaintenanceCardCommandHandler(
        IMaintenanceCardRepository maintenanceCardRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateMaintenanceCardCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateMaintenanceCardCommand request, CancellationToken cancellationToken)
        {
            var card = new MaintenanceCard
            {
                Name = request.Name,
                Description = request.Description,
                ProductId = request.ProductId,
                PeriodInDays = request.PeriodInDays,
                LastMaintenanceDate = request.LastMaintenanceDate,
                NextMaintenanceDate = request.LastMaintenanceDate.AddDays(request.PeriodInDays),
                AssignedEmail = request.AssignedEmail
            };

            await maintenanceCardRepository.AddAsync(card, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Bakım kartı başarıyla oluşturuldu";
        }
    }
}
