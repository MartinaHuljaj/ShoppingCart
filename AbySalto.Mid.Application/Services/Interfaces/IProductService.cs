using AbySalto.Mid.Application.DTO;

namespace AbySalto.Mid.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductsResponse> GetProductsAsync(int limit, int skip);
        Task<ProductDto?> GetProductByIdAsync(int productId);
    }
}
