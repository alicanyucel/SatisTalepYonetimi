using FluentValidation;

namespace SatisTalepYonetimi.Application.Features.Customers.CreateCustomer
{
    public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Müşteri adı boş olamaz");
            RuleFor(p => p.Name).MaximumLength(200).WithMessage("Müşteri adı en fazla 200 karakter olabilir");
        }
    }
}
