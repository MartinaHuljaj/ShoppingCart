using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Application.Mappers.Interfaces;
using AbySalto.Mid.Application.Services.Interfaces;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Application.Services
{
    public class FavoritesService: IFavoritesService
    {
        private readonly IMemoryCache _cache;
        private readonly IProductRepository _productRepository;
        private readonly IProductMapper _productMapper;
        private readonly IProductService _productService;

        public FavoritesService(IMemoryCache cache, IProductRepository productRepository, IProductMapper productMapper, IProductService productService)
        {
            _cache = cache;
            _productRepository = productRepository;
            _productMapper = productMapper;
            _productService = productService;
        }

        public async Task<IEnumerable<ProductDto>> GetFavoritesAsync(string userId)
        {
            var cacheKey = $"favorites:{userId}";
            if (_cache.TryGetValue(cacheKey, out IEnumerable<ProductDto> products))
            {
                return products;
            }

            var favoriteItems = await _productRepository.GetFavoritesAsync(userId);
            if (favoriteItems.Count() != 0)
            {
                var favorites = _productMapper.FavoriteItemsToDtos(favoriteItems);
                _cache.Set(cacheKey, favorites, TimeSpan.FromMinutes(10));
                return favorites;
            }
           
            return new List<ProductDto>();


        }

        public async Task<IActionResult> AddToFavoritesAsync(string userId, int productId)
        {
            var newProduct = await _productService.GetProductByIdAsync(productId);
            var mappedProduct = _productMapper.ProductDtoToEntity(newProduct);
            await _productRepository.AddToFavoritesAsync(userId, mappedProduct);
            var cacheKey = $"favorites:{userId}";
            var favorites = _cache.TryGetValue(cacheKey, out List<ProductDto> products) ? products : new List<ProductDto>();
            if (favorites.Count == 0)
            {
                var existingFavorites = await _productRepository.GetFavoritesAsync(userId);
                favorites = _productMapper.FavoriteItemsToDtos(existingFavorites);
            }
            if (newProduct != null && favorites.All(f => f.Id != productId))
            {
                favorites.Add(newProduct);
                _cache.Set(cacheKey, favorites, TimeSpan.FromMinutes(10));
            }
            return new OkObjectResult(new { Message = "Product added to favorites successfully." });
        }
        public async Task<IActionResult> RemoveFromFavoritesAsync(string userId, int productId)
        {
            await _productRepository.RemoveItemFromFavoritesAsync(productId, userId);
            var cacheKey = $"favorites:{userId}";
            if (_cache.TryGetValue(cacheKey, out List<ProductDto> products))
            {
                var productToRemove = products.FirstOrDefault(p => p.Id == productId);
                if (productToRemove != null)
                {
                    products.Remove(productToRemove);
                    _cache.Set(cacheKey, products, TimeSpan.FromMinutes(10));
                }
            }
            return new OkObjectResult(new { Message = "Product removed from favorites successfully." });
        }
    }
}
