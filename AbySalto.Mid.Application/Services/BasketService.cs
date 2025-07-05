using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Application.Mappers.Interfaces;
using AbySalto.Mid.Application.Services.Interfaces;
using AbySalto.Mid.Application.Validators;
using AbySalto.Mid.Domain.Entities;
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
        private readonly AddToBasketValidator _addValidator;
        private readonly RemoveFromBasketValidator _removeValidator;
        public BasketService(IProductRepository productRepository, IProductMapper mapper, IProductService productService, IMemoryCache cache, AddToBasketValidator validator, RemoveFromBasketValidator removeValidator)
        {
            _productRepository = productRepository;
            _productMapper = mapper;
            _productService = productService;
            _cache = cache;
            _addValidator = validator;
            _removeValidator = removeValidator;
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

            _addValidator.Validate(userId, quantity, productDto);

            var productEntity = _productMapper.ProductDtoToEntity(productDto);

            await _productRepository.AddToBasketAsync(userId, productEntity, quantity);
            var basketItem = await _productRepository.GetBasketItemAsync(userId, productId);

            await UpdateBasketCache(userId, basketItem);

            return new OkObjectResult(new { Message = "Product added to basket successfully." });
        }

        public async Task<IActionResult> RemoveFromBasketAsync(string userId, int productId)
        {
            var basketItem = await _productRepository.GetBasketItemAsync(userId, productId);
            _removeValidator.Validate(basketItem, userId);
            await _productRepository.RemoveItemFromBasketAsync(productId, userId);
            var cacheKey = $"basket:{userId}";
            if (_cache.TryGetValue(cacheKey, out List<BasketDto> cachedBasket))
            {
                cachedBasket.RemoveAll(b => b.Id == productId);
                _cache.Set(cacheKey, cachedBasket, TimeSpan.FromMinutes(10));
            }
            return new OkObjectResult(new { Message = "Product removed from basket successfully." });
        }

        private async Task UpdateBasketCache(string userId, BasketItem basketItem)
        {
            var cacheKey = $"basket:{userId}";

            if (basketItem == null) return;

            if (!_cache.TryGetValue(cacheKey, out List<BasketDto> basket))
            {
                var dbItems = await _productRepository.GetBasketAsync(userId);
                basket = _productMapper.BasketItemsToDtos(dbItems);
            }

            var newBasketDto = _productMapper.BasketItemToDtos(basketItem);
            var index = basket.FindIndex(b => b.Id == basketItem.ProductId);

            if (index >= 0)
                basket[index] = newBasketDto;
            else
                basket.Add(newBasketDto);

            _cache.Set(cacheKey, basket, TimeSpan.FromMinutes(10));
        }

    }
}
