using AbySalto.Mid.Application.DTO;
using FluentValidation;

namespace AbySalto.Mid.Application.Validators
{
    public class BasketDtoValidator: AbstractValidator<BasketDto>
    {
        public BasketDtoValidator()
        {
            RuleFor(b => b.Quantity)
                .NotEmpty().GreaterThan(0);
        }
    }
}
