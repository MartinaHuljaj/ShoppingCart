using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.ValidationMessages;
using AbySalto.Mid.Infrastructure.Context;
using AbySalto.Mid.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Infrastructure.Repositories
{
    public class FavoritesRepository: IFavoritesRepository
    {
        private readonly AbySaltoDbContext _context;
        public FavoritesRepository(AbySaltoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FavoriteItem>> GetFavoritesAsync(string userId)
        {
            try
            {
                return await _context.Set<FavoriteItem>()
                    .Include(fi => fi.Product)
                    .Where(fi => fi.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving favorite products.");
            }
        }

        public async Task AddToFavoritesAsync(string userId, Product product)
        {
            try
            {
                var exists = await _context.Set<FavoriteItem>()
                    .AnyAsync(f => f.ProductId == product.ProductId && f.UserId == userId);

                if (exists)
                    throw new InvalidOperationException(ValidationMessages.ProductAlreadyInFavorites);

                var favoriteItem = new FavoriteItem
                {
                    UserId = userId,
                    ProductId = product.ProductId
                };
                var productExists = await GetProductAsync(product.ProductId);
                if (productExists == null)
                {
                    _context.Set<Product>().Add(product);
                    await _context.SaveChangesAsync();
                }

                _context.Set<FavoriteItem>().Add(favoriteItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occured while adding to favorites. {ex.Message}", ex);
            }
        }

        public async Task RemoveItemFromFavoritesAsync(int productId, string userId)
        {
            try
            {
                var favoriteItem = await _context.Set<FavoriteItem>()
                    .FirstOrDefaultAsync(f => f.ProductId == productId && f.UserId == userId);
                if (favoriteItem != null)
                {
                    _context.Set<FavoriteItem>().Remove(favoriteItem);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException(ValidationMessages.ProductNotInFavorites);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while removing the item from favorites. {ex.Message}", ex);
            }
        }

        public async Task<Product?> GetProductAsync(int productId)
        {
            var t = await _context.Set<Product>()
                .Where(p => p.ProductId == productId)
                .FirstOrDefaultAsync();
            return await _context.Set<Product>()
                .Where(p => p.ProductId == productId)
                .FirstOrDefaultAsync();
        }
    }
}
