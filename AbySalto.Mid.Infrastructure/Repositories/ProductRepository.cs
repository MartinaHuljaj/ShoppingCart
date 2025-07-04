using AbySalto.Mid.Domain.DatabaseContext;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Mid.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AbySaltoDbContext _context;
        private readonly UserManager<User> _userManager;
        public ProductRepository(IDbContextFactory<AbySaltoDbContext> context, UserManager<User> userManager)
        {
            _context = context.CreateDbContext();
            _userManager = userManager;
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

        public async Task<IEnumerable<BasketItem>> GetBasketAsync(string userId)
        {
            try
            {
                return await _context.Set<BasketItem>()
                    .Include(bi => bi.Product)
                    .Where(bi => bi.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while retrieving basket items. {ex.Message}", ex);
            }
        }

        public async Task<BasketItem> GetBasketItemAsync(string userId, int productId)
        {
            try
            {
                return await _context.Set<BasketItem>()
                    .Include(bi => bi.Product)
                    .Include(bi => bi.User)
                    .Where(bi => bi.UserId == userId && bi.ProductId == productId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while retrieving the basket item. {ex.Message}", ex);
            }
        }

        public async Task AddToFavoritesAsync(string userId, Product product)
        {
            try
            {
                var exists = await _context.Set<FavoriteItem>()
                    .AnyAsync(f => f.ProductId == product.ProductId && f.UserId == userId);

                if (exists)
                    throw new InvalidOperationException("The product is already in the user's favorites.");

                var favoriteItem = new FavoriteItem
                {
                    UserId = userId,
                    ProductId = product.ProductId
                };
                _context.Set<Product>().Add(product);
                await _context.SaveChangesAsync();

                _context.Set<FavoriteItem>().Add(favoriteItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{ex.Message}", ex);
            }
        }

        public async Task AddToBasketAsync(string userId, Product product)
        {
            try
            {
                var item = await _context.Set<BasketItem>()
                    .Where(f => f.ProductId == product.ProductId && f.UserId == userId)
                    .FirstOrDefaultAsync();

                if (item != null)
                {
                    item.Quantity++;
                    _context.Set<BasketItem>().Update(item);
                }
                else
                {
                    var basketItem = new BasketItem
                    {
                        UserId = userId,
                        ProductId = product.ProductId,
                        Quantity = 1
                    };
                    _context.Set<Product>().Add(product);
                    await _context.SaveChangesAsync();
                    _context.Set<BasketItem>().Add(basketItem);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while retrieving basket items. {ex.Message}", ex);
            }
        }
    }
}
