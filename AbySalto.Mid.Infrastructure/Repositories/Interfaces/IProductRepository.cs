using AbySalto.Mid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<FavoriteItem>> GetFavoritesAsync(string userId);
        Task<IEnumerable<BasketItem>> GetBasketAsync(string userId);
        Task<BasketItem> GetBasketItemAsync(string userId, int productId);
        Task AddToFavoritesAsync(string userId, Product product);
        Task AddToBasketAsync(string userId, Product product);
        Task RemoveItemFromFavoritesAsync(int productId, string userId);
        Task RemoveItemFromBasketAsync(int productId, string userId);
    }
}
