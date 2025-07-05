using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Application.Mappers.Interfaces;
using AbySalto.Mid.Application.Services.Interfaces;
using AbySalto.Mid.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AbySalto.Mid.Application.Services
{
    public class BasketService: IBasketService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductMapper _productMapper;
        private readonly IProductService _productService;
        private readonly IMemoryCache _cache;
        public BasketService(IProductRepository productRepository, IProductMapper mapper, IProductService productService, IMemoryCache cache)
        {
            _productRepository = productRepository;
            _productMapper = mapper;
            _productService = productService;
            _cache = cache;
        }
        public async Task<IEnumerable<BasketDto>> GetBasketAsync(string userId)
        {
            var cacheKey = $"basket:{userId}";
            if (_cache.TryGetValue(cacheKey, out IEnumerable<BasketDto> products))
            {
                return products;
            }

            var basketItems = await _productRepository.GetBasketAsync(userId);
            if (basketItems.Count() != 0)
            {
                var basket = _productMapper.BasketItemsToDtos(basketItems);
                _cache.Set(cacheKey, basket, TimeSpan.FromMinutes(10));
                return basket;
            }

            return new List<BasketDto>();
        }

        public async Task<IActionResult> AddToBasketAsync(string userId, int productId, int quantity)
        {
            var productDto = await _productService.GetProductByIdAsync(productId);
            if (productDto == null || productDto.Stock < quantity)
            {
                throw new InvalidOperationException("There is not enoguh product in stock");
            }
            var productEntity = _productMapper.ProductDtoToEntity(productDto);
            await _productRepository.AddToBasketAsync(userId, productEntity, quantity);
            var basketItem =await _productRepository.GetBasketItemAsync(userId, productId);

            var cacheKey = $"basket:{userId}";

            var basket = _cache.TryGetValue(cacheKey, out List<BasketDto> cachedBasket)
                ? cachedBasket
                : new List<BasketDto>();

            if (basket.Count == 0)
            {
                var dbBasketItems = await _productRepository.GetBasketAsync(userId);
                basket = _productMapper.BasketItemsToDtos(dbBasketItems);
            }

            BasketDto alreadyInBasket = basket.FirstOrDefault(b => b.Id == productId);
            if (basketItem != null)
            {
                var newBasketDto = _productMapper.BasketItemToDtos(basketItem);

                if (alreadyInBasket != null)
                {
                    int index = basket.FindIndex(b => b.Id == productId);
                    if (index != -1)
                    {
                        basket[index] = newBasketDto;
                    }
                }
                else
                {
                    basket.Add(newBasketDto);
                }

                _cache.Set(cacheKey, basket, TimeSpan.FromMinutes(10));
            }

            return new OkObjectResult(new { Message = "Product added to basket successfully." });
        }

        public async Task<IActionResult> RemoveFromBasketAsync(string userId, int productId)
        {
            var basketItem = await _productRepository.GetBasketItemAsync(userId, productId);
            if (basketItem == null)
            {
                return new NotFoundObjectResult(new { Message = "Product not found in basket." });
            }
            await _productRepository.RemoveItemFromBasketAsync(productId, userId);
            var cacheKey = $"basket:{userId}";
            if (_cache.TryGetValue(cacheKey, out List<BasketDto> cachedBasket))
            {
                cachedBasket.RemoveAll(b => b.Id == productId);
                _cache.Set(cacheKey, cachedBasket, TimeSpan.FromMinutes(10));
            }
            return new OkObjectResult(new { Message = "Product removed from basket successfully." });
        }

    }
}
