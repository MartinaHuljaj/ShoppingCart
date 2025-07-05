namespace AbySalto.Mid.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; } = default!;
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Brand { get; set; }
    }
}
