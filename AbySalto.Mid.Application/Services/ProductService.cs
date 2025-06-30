using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Application.Services.Interfaces;
using System.Net.Http.Json;

namespace AbySalto.Mid.Application.Services
{
    public class ProductService: IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ProductsResponse> GetProductsAsync(int limit, int skip)
        {
            var client = _httpClientFactory.CreateClient("ProductApi");
            string url = $"products?limit={limit}&skip={skip}";
            var response = await client.GetFromJsonAsync<ProductsResponse>(url);
            return response ?? throw new Exception("Failed to retrieve products.");
        }

        public async Task<ProductDto?> GetProductByIdAsync(int productId) {             
            var client = _httpClientFactory.CreateClient("ProductApi");
            string url = $"products/{productId}";
            var response = await client.GetFromJsonAsync<ProductDto>(url);
            return response ?? null;
        }
    }
}
