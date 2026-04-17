using FluentValidation;

namespace SatisTalepYonetimi.Application.Features.MaintenanceCards.CreateMaintenanceCard
{
    public sealed class CreateMaintenanceCardCommandValidator : AbstractValidator<CreateMaintenanceCardCommand>
    {
        public CreateMaintenanceCardCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Bakım kartı adı boş olamaz");
            RuleFor(p => p.ProductId).NotEmpty().WithMessage("Ürün seçilmelidir");
            RuleFor(p => p.PeriodInDays).GreaterThan(0).WithMessage("Periyot 0'dan büyük olmalıdır");
            RuleFor(p => p.LastMaintenanceDate).NotEmpty().WithMessage("Son bakım tarihi belirtilmelidir");
        }
    }
}
