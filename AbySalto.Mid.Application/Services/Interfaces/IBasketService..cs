using AbySalto.Mid.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.Application.Services.Interfaces
{
    public interface IBasketService
    {
        Task<IEnumerable<BasketDto>> GetBasketAsync(string userId);
        Task<IActionResult> AddToBasketAsync(string userId, int productId);
    }
}
