using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Application.Mappers.Interfaces;
using AbySalto.Mid.Application.Services.Interfaces;
using AbySalto.Mid.Application.Validators;
using AbySalto.Mid.Infrastructure.Repositories;
using AbySalto.Mid.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AbySalto.Mid.Application.Services
{
    public class FavoritesService: IFavoritesService
    {
        private readonly IFavoritesRepository _favoritesRepository;
        private readonly IProductMapper _productMapper;
        private readonly IProductService _productService;
        private readonly FavoritesValidator _favoritesValidator;
        public FavoritesService(IFavoritesRepository favoritesRepository, IProductMapper productMapper, IProductService productService, FavoritesValidator favoritesValidator)
        {
            _favoritesRepository = favoritesRepository;
            _productMapper = productMapper;
            _productService = productService;
            _favoritesValidator = favoritesValidator;
        }

        public async Task<IEnumerable<ProductDto>> GetFavoritesAsync(string userId)
        {
            var favoriteItems = await _favoritesRepository.GetFavoritesAsync(userId);
            if (favoriteItems.Count() != 0)
            {
                var favorites = _productMapper.FavoriteItemsToDtos(favoriteItems);
                return favorites;
            }
           
            return new List<ProductDto>();


        }

        public async Task<IActionResult> AddToFavoritesAsync(string userId, int productId)
        {
            var newProduct = await _productService.GetProductByIdAsync(productId);
            _favoritesValidator.Validate(userId, newProduct);
            var mappedProduct = _productMapper.ProductDtoToEntity(newProduct);
            await _favoritesRepository.AddToFavoritesAsync(userId, mappedProduct);
            return new OkObjectResult(new { Message = "Product added to favorites successfully." });
        }
        public async Task<IActionResult> RemoveFromFavoritesAsync(string userId, int productId)
        {
            _favoritesValidator.Validate(userId, productId);
            await _favoritesRepository.RemoveItemFromFavoritesAsync(productId, userId);
            return new OkObjectResult(new { Message = "Product removed from favorites successfully." });
        }
    }
}
