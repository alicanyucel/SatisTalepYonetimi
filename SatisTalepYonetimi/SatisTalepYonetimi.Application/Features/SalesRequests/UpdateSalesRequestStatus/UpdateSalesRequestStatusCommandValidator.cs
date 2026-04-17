using FluentValidation;
using SatisTalepYonetimi.Domain.Enums;

namespace SatisTalepYonetimi.Application.Features.SalesRequests.UpdateSalesRequestStatus
{
    public sealed class UpdateSalesRequestStatusCommandValidator : AbstractValidator<UpdateSalesRequestStatusCommand>
    {
        public UpdateSalesRequestStatusCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Talep Id boş olamaz");
            RuleFor(p => p.StatusValue)
                .Must(v => SalesRequestStatusEnum.TryFromValue(v, out _))
                .WithMessage("Geçersiz durum değeri");
        }
    }
}
