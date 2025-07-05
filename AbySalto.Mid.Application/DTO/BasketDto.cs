namespace AbySalto.Mid.Application.DTO
{
    public class BasketDto: ProductDto
    {
        public int Quantity { get; set; }
        public BasketDto(int productId, string title, string description, string category, decimal price, int stock, string brand, int quantity)
            : base(productId, title, description, category, price, stock, brand)
        {
            Quantity = quantity;
        }
    }
}
