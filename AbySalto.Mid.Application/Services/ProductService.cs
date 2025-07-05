using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Application.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;

namespace AbySalto.Mid.Application.Services
{
    public class ProductService: IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;
        public ProductService(IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        public async Task<ProductsResponse> GetProductsAsync(int limit, int skip, string sortBy, string order)
        {
            sortBy = sortBy.ToLower();
            order = order.ToLower();
            string cacheKey = $"products:limit={limit}:skip={skip}:sortBy={sortBy}:sortOrder={order}";
            if (!_cache.TryGetValue(cacheKey, out ProductsResponse productsResponse))
            {
                var client = _httpClientFactory.CreateClient("ProductApi");
                string url = $"products?limit={limit}&skip={skip}&sortBy={sortBy}&order={order}";
                productsResponse = await client.GetFromJsonAsync<ProductsResponse>(url);
                if (productsResponse != null)
                {
                    _cache.Set(cacheKey, productsResponse, TimeSpan.FromMinutes(10));
                }
                else
                {
                    throw new Exception("Failed to retrieve products.");
                }
            }
            return productsResponse;
        }

        public async Task<ProductDto?> GetProductByIdAsync(int productId)
        {
            string cacheKey = $"product:{productId}";
            if (!_cache.TryGetValue(cacheKey, out ProductDto product))
            {
                var client = _httpClientFactory.CreateClient("ProductApi");
                string url = $"products/{productId}";
                product = await client.GetFromJsonAsync<ProductDto>(url);
                if (product != null)
                {
                    _cache.Set(cacheKey, product, TimeSpan.FromMinutes(10));
                }
            }
            return product;
        }
        public async Task<IEnumerable<ProductDto>> GetProductListByIdsAsync(IEnumerable<int> productIds)
        {
            var client = _httpClientFactory.CreateClient("ProductApi");
            var products = new List<ProductDto>();

            foreach (var productId in productIds)
            {
                string cacheKey = $"product:{productId}";
                if (!_cache.TryGetValue(cacheKey, out ProductDto product))
                {
                    string url = $"products/{productId}";
                    product = await client.GetFromJsonAsync<ProductDto>(url);
                    if (product != null)
                    {
                        _cache.Set(cacheKey, product, TimeSpan.FromMinutes(10));
                    }
                }
                if (product != null)
                {
                    products.Add(product);
                }
            }

            return products;
        }
    }
}
