using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Application.Services.Interfaces;
using AbySalto.Mid.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly UserManager<User> _userManager;
        public BasketController(IBasketService basketService, UserManager<User> userManager)
        {
            _basketService = basketService;
            _userManager = userManager;
        }


        [HttpGet("basket")]
        [Authorize]
        public async Task<IEnumerable<BasketDto>> GetBasket()
        {
            var userId = _userManager.GetUserId(User);
            var result = await _basketService.GetBasketAsync(userId);
            return result;
        }

        [HttpPost("basket/{productId}")]
        [Authorize]
        public async Task<IActionResult> AddToBasketAsync([FromRoute] int productId, int quantity = 1)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _basketService.AddToBasketAsync(userId, productId, quantity);
            return result;
        }
        [HttpDelete("basket/{productId}")]
        [Authorize]
        public async Task<IActionResult> RemoveFromBasketAsync([FromRoute] int productId)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _basketService.RemoveFromBasketAsync(userId, productId);
            return result;
        }
    }
}
