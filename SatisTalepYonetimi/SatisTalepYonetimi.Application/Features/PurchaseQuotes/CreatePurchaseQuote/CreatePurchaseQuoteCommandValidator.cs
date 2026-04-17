using FluentValidation;

namespace SatisTalepYonetimi.Application.Features.PurchaseQuotes.CreatePurchaseQuote
{
    public sealed class CreatePurchaseQuoteCommandValidator : AbstractValidator<CreatePurchaseQuoteCommand>
    {
        public CreatePurchaseQuoteCommandValidator()
        {
            RuleFor(p => p.SalesRequestId).NotEmpty().WithMessage("Satış talebi seçilmelidir");
            RuleFor(p => p.SupplierId).NotEmpty().WithMessage("Tedarikçi seçilmelidir");
            RuleFor(p => p.TotalAmount).GreaterThan(0).WithMessage("Teklif tutarı 0'dan büyük olmalıdır");
        }
    }
}
