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
        private readonly IBasketRepository _basketRepository;
        private readonly IProductMapper _productMapper;
        private readonly IProductService _productService;
        private readonly AddToBasketValidator _addValidator;
        private readonly RemoveFromBasketValidator _removeValidator;
        public BasketService(IBasketRepository basketRepository, IProductMapper mapper, IProductService productService, AddToBasketValidator validator, RemoveFromBasketValidator removeValidator)
        {
            _basketRepository = basketRepository;
            _productMapper = mapper;
            _productService = productService;
            _addValidator = validator;
            _removeValidator = removeValidator;
        }
        public async Task<IEnumerable<BasketDto>> GetBasketAsync(string userId)
        {
            var basketItems = await _basketRepository.GetBasketAsync(userId);
            if (basketItems.Count() != 0)
            {
                var basket = _productMapper.BasketItemsToDtos(basketItems);
                return basket;
            }

            return new List<BasketDto>();
        }

        public async Task<IActionResult> AddToBasketAsync(string userId, int productId, int quantity)
        {
            var productDto = await _productService.GetProductByIdAsync(productId);

            _addValidator.Validate(userId, quantity, productDto);

            var productEntity = _productMapper.ProductDtoToEntity(productDto);

            await _basketRepository.AddToBasketAsync(userId, productEntity, quantity);
            var basketItem = await _basketRepository.GetBasketItemAsync(userId, productId);

            return new OkObjectResult(new { Message = "Product added to basket successfully." });
        }

        public async Task<IActionResult> RemoveFromBasketAsync(string userId, int productId)
        {
            var basketItem = await _basketRepository.GetBasketItemAsync(userId, productId);
            _removeValidator.Validate(basketItem, userId);
            await _basketRepository.RemoveItemFromBasketAsync(productId, userId);

            return new OkObjectResult(new { Message = "Product removed from basket successfully." });
        }
    }
}
