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
    public class BasketRepository: IBasketRepository
    {
        private readonly AbySaltoDbContext _context;
        public BasketRepository(AbySaltoDbContext context)
        {
            _context = context;
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

        public async Task AddToBasketAsync(string userId, Product product, int quantity)
        {
            try
            {
                var item = await _context.Set<BasketItem>()
                    .Include(bi => bi.Product)
                    .Where(bi => bi.ProductId == product.ProductId && bi.UserId == userId)
                    .FirstOrDefaultAsync();
                if (item != null)
                {
                    if (item.Product.Stock < item.Quantity + quantity)
                    {
                        throw new InvalidOperationException(ValidationMessages.ProductOutOfStock);
                    }
                    item.Quantity += quantity;
                    _context.Set<BasketItem>().Update(item);
                }
                else
                {
                    var basketItem = new BasketItem
                    {
                        UserId = userId,
                        ProductId = product.ProductId,
                        Quantity = quantity
                    };
                    var productExists = await GetProductAsync(product.ProductId);
                    if (productExists == null)
                    {
                        _context.Set<Product>().Add(product);
                        await _context.SaveChangesAsync();
                    }

                    _context.Set<BasketItem>().Add(basketItem);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while adding to basket. {ex.Message}", ex);
            }
        }



        public async Task RemoveItemFromBasketAsync(int productId, string userId)
        {
            try
            {
                var basketItem = await _context.Set<BasketItem>()
                    .FirstOrDefaultAsync(b => b.ProductId == productId && b.UserId == userId);
                if (basketItem != null)
                {
                    _context.Set<BasketItem>().Remove(basketItem);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException(ValidationMessages.ProductNotInBasket);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while removing the item from the basket. {ex.Message}", ex);
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
