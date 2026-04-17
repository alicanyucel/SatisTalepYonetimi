using FluentValidation;

namespace SatisTalepYonetimi.Application.Features.Suppliers.CreateSupplier
{
    public sealed class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Tedarikçi adı boş olamaz");
            RuleFor(p => p.Email).NotEmpty().EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz");
            RuleFor(p => p.Phone).NotEmpty().WithMessage("Telefon numarası boş olamaz");
        }
    }
}
