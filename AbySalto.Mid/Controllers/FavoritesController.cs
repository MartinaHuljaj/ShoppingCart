using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Application.Services.Interfaces;
using AbySalto.Mid.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AbySalto.Mid.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IFavoritesService _favoritesService;

        public FavoritesController(UserManager<User> userManager, IFavoritesService favoritesService)
        {
            _userManager = userManager;
            _favoritesService = favoritesService;
        }

        [HttpGet("favorites")]
        [Authorize]
        public async Task<IEnumerable<ProductDto>> GetFavorites()
        {
            var userId = _userManager.GetUserId(User);
            var result = await _favoritesService.GetFavoritesAsync(userId);
            return result;
        }

        [HttpPost("favorites/{productId}")]
        [Authorize]
        public async Task<IActionResult> AddToFavorites([FromRoute] int productId)
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "User not authenticated." });
            }
            var result = await _favoritesService.AddToFavoritesAsync(userId, productId);
            return result;
        }
    }
}
