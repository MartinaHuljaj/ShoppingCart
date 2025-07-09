using AbySalto.Mid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Infrastructure.Repositories.Interfaces
{
    public interface IFavoritesRepository
    {
        Task<IEnumerable<FavoriteItem>> GetFavoritesAsync(string userId);
        Task AddToFavoritesAsync(string userId, Product product);
        Task RemoveItemFromFavoritesAsync(int productId, string userId);
        Task<Product?> GetProductAsync(int productId);
    }
}
