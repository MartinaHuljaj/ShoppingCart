using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Domain.ValidationMessages;

namespace AbySalto.Mid.Application.Validators
{
    public class FavoritesValidator
    {
        public void Validate(string userId, ProductDto product)
        {
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException(ValidationMessages.UnauthorizedUser);
            if (product == null)
                throw new InvalidOperationException(ValidationMessages.ProductNotFound);
        }
        public void Validate(string userId, int productId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new InvalidOperationException(ValidationMessages.UnauthorizedUser);
            if (productId <= 0)
                throw new InvalidOperationException(ValidationMessages.ProductIdLessThenZero);
        }
    }
}
