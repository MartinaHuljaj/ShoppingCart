using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.ValidationMessages;

namespace AbySalto.Mid.Application.Validators
{
    public class RemoveFromBasketValidator
    {
        public void Validate(BasketItem item, string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException(ValidationMessages.UnauthorizedUser);

            if (item == null)
                throw new InvalidOperationException(ValidationMessages.ProductNotInBasket);
        }
    }
}
