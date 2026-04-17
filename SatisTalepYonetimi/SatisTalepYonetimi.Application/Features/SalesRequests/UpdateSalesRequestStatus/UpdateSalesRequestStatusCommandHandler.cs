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

            salesRequest.StatusValue = request.StatusValue;

            if (request.StatusValue == SalesRequestStatusEnum.Approved.Value)
                salesRequest.ApprovedDate = DateTime.UtcNow;

            salesRequestRepository.Update(salesRequest);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var status = SalesRequestStatusEnum.FromValue(request.StatusValue);
            return $"Satış talebi durumu '{status.Name}' olarak güncellendi";
        }
    }
}
