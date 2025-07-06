using AbySalto.Mid.Application.DTO;
using FluentValidation;
using AbySalto.Mid.Domain.ValidationMessages;

namespace AbySalto.Mid.Application.Validators
{
    public class ProductDtoValidator: AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(b => b.Title).MaximumLength(100).NotNull();
            RuleFor(b => b.Description).MaximumLength(500).NotNull();
            RuleFor(b => b.Category).MaximumLength(50).NotNull();
            RuleFor(b => b.Price).GreaterThan(0).NotNull();
            RuleFor(b => b.Stock).GreaterThan(0).NotNull().WithMessage(ValidationMessages.ProductOutOfStock);
            RuleFor(b => b.Brand).MaximumLength(50).NotNull();
        }
    }
}
