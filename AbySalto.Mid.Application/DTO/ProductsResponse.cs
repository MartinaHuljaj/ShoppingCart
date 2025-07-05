namespace AbySalto.Mid.Application.DTO
{
    public class ProductsResponse
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public int Total { get; set; }
        public int Skip { get; set; }
        public int Limit { get; set; }
    }
}
