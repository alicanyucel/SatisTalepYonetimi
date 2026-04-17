using FluentValidation;

namespace SatisTalepYonetimi.Application.Features.Customers.UpdateCustomer
{
    public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Müşteri Id boş olamaz");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Müşteri adı boş olamaz");
            RuleFor(p => p.Name).MaximumLength(200).WithMessage("Müşteri adı en fazla 200 karakter olabilir");
        }
    }
}
