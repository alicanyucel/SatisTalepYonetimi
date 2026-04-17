using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Enums;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.SalesRequests.UpdateSalesRequestStatus
{
    internal sealed class UpdateSalesRequestStatusCommandHandler(
        ISalesRequestRepository salesRequestRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateSalesRequestStatusCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateSalesRequestStatusCommand request, CancellationToken cancellationToken)
        {
            var salesRequest = await salesRequestRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
            if (salesRequest is null)
                return (500, "Satış talebi bulunamadı");

            var currentStatus = salesRequest.StatusValue;
            var newStatus = request.StatusValue;

            bool isValidTransition = (currentStatus, newStatus) switch
            {
                (1, 2) => true,  // Beklemede → Yönetici Onayında
                (2, 3) => true,  // Yönetici Onayında → Yönetici Onayladı
                (2, 4) => true,  // Yönetici Onayında → Reddedildi
                (3, 5) => true,  // Yönetici Onayladı → Satınalma Sürecinde
                (8, 9) => true,  // Teklif Onaylandı → Tamamlandı
                (_, 10) => true, // Herhangi bir durumdan → İptal Edildi
                _ => false
            };

            if (!isValidTransition)
                return (500, $"Geçersiz durum geçişi: {SalesRequestStatusEnum.FromValue(currentStatus).Name} → {SalesRequestStatusEnum.FromValue(newStatus).Name}");

            salesRequest.StatusValue = newStatus;

            if (newStatus == SalesRequestStatusEnum.ManagerApproved.Value)
                salesRequest.ApprovedDate = DateTime.UtcNow;

            salesRequestRepository.Update(salesRequest);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var status = SalesRequestStatusEnum.FromValue(newStatus);
            return $"Satış talebi durumu '{status.Name}' olarak güncellendi";
        }
    }
}
