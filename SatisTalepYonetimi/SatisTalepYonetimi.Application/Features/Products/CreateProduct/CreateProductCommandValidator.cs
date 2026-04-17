using FluentValidation;

namespace SatisTalepYonetimi.Application.Features.Products.CreateProduct
{
    public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Ürün adı boş olamaz");
            RuleFor(p => p.Code).NotEmpty().WithMessage("Ürün kodu boş olamaz");
            RuleFor(p => p.UnitPrice).GreaterThan(0).WithMessage("Birim fiyat 0'dan büyük olmalıdır");
        }
    }
}
