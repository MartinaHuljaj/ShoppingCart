using AbySalto.Mid.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.Application.Services.Interfaces
{
    public interface IFavoritesService
    {
        Task<IEnumerable<ProductDto>> GetFavoritesAsync(string userId);
        Task<IActionResult> AddToFavoritesAsync(string userId, int productId);
        Task<IActionResult> RemoveFromFavoritesAsync(string userId, int productId);
    }
}
