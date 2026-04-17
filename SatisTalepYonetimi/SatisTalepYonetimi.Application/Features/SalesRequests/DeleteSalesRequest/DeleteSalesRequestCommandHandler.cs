using GenericRepository;
using MediatR;
using SatisTalepYonetimi.Domain.Repositories;
using TS.Result;

namespace SatisTalepYonetimi.Application.Features.SalesRequests.DeleteSalesRequest
{
    internal sealed class DeleteSalesRequestCommandHandler(
        ISalesRequestRepository salesRequestRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteSalesRequestCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteSalesRequestCommand request, CancellationToken cancellationToken)
        {
            var salesRequest = await salesRequestRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
            if (salesRequest is null)
                return (500, "Satış talebi bulunamadı");

            salesRequestRepository.Delete(salesRequest);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Satış talebi başarıyla silindi";
        }
    }
}
