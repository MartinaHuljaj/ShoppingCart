using AbySalto.Mid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbySalto.Mid.Infrastructure.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<IEnumerable<BasketItem>> GetBasketAsync(string userId);
        Task<BasketItem> GetBasketItemAsync(string userId, int productId);
        Task AddToBasketAsync(string userId, Product product, int quantity);

        Task RemoveItemFromBasketAsync(int productId, string userId);
        Task<Product?> GetProductAsync(int productId);
    }
}
