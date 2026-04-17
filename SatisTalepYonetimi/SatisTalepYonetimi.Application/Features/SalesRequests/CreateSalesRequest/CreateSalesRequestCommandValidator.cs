using FluentValidation;

namespace SatisTalepYonetimi.Application.Features.SalesRequests.CreateSalesRequest
{
    public sealed class CreateSalesRequestCommandValidator : AbstractValidator<CreateSalesRequestCommand>
    {
        public CreateSalesRequestCommandValidator()
        {
            RuleFor(p => p.CustomerId).NotEmpty().WithMessage("Müşteri seçilmelidir");
            RuleFor(p => p.RequestedByUserId).NotEmpty().WithMessage("Talep eden kullanıcı belirtilmelidir");
            RuleFor(p => p.Items).NotEmpty().WithMessage("En az bir ürün kalemi eklenmelidir");
            RuleForEach(p => p.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId).NotEmpty().WithMessage("Ürün seçilmelidir");
                item.RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır");
            });
        }
    }
}
