using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Domain.ValidationMessages;

namespace AbySalto.Mid.Application.Validators
{
    public class AddToBasketValidator
    {
        public void Validate(string userId, int quantity, ProductDto productDto)
        {
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException(ValidationMessages.UnauthorizedUser);

            if (quantity <= 0)
                throw new InvalidOperationException(ValidationMessages.QuantityGreaterThanZero);

            if (productDto == null || productDto.Stock < quantity)
                throw new InvalidOperationException(ValidationMessages.ProductOutOfStock);
        }
    }
}
