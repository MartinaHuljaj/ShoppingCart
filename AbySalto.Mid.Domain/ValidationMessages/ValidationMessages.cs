namespace AbySalto.Mid.Domain.ValidationMessages
{
    public class ValidationMessages
    {
        public const string Required = "This field is required.";
        public const string QuantityGreaterThanZero = "Quantity must be greater than zero.";
        public const string ProductOutOfStock = "There is not enough product in stock for this action.";
        public const string UnauthorizedUser = "User not authorized.";
        public const string ProductNotFound = "Product not found";
        public const string ProductIdLessThenZero = "Product Id must be greater then zero.";
        public const string ProductNotInBasket = "Product not found in basket";
        public const string ProductNotInFavorites = "Product not found in favorites";
        public const string ProductAlreadyInFavorites = "Product already in favorites.";
        public const string ProductFetchFailed = "Failed to retrieve products.";
        public const string InvalidEmailFormat = "Invalid email format.";
    }
}
