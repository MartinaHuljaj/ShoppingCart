using AbySalto.Mid.Application.DTO;

namespace AbySalto.Mid.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductsResponse> GetProductsAsync(int limit, int skip, string sortBy, string order);
        Task<ProductDto?> GetProductByIdAsync(int productId);
        Task<IEnumerable<ProductDto>> GetProductListByIdsAsync(IEnumerable<int> productIds);
    }
}
