using AbySalto.Mid.Application.Services.Interfaces;
using AbySalto.Mid.Application.Validators;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.ValidationMessages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly UserManager<User> _userManager;
        public ProductController(IProductService productService, UserManager<User> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts(
            [FromQuery] int limit = 10,
            [FromQuery] int skip = 0,
            [FromQuery] string sortBy = "id",
            [FromQuery] string order = "asc")
        {
            var result = await _productService.GetProductsAsync(limit, skip, sortBy, order);
            return Ok(result);
        }
        [HttpGet("products/{productId}")]
        public async Task<IActionResult> GetProductById([FromRoute] int productId)
        {
            var result = await _productService.GetProductByIdAsync(productId);
            if (result == null)
            {
                return NotFound(new { Message = ValidationMessages.ProductNotFound });
            }
            return Ok(result);
        }
    }
}
