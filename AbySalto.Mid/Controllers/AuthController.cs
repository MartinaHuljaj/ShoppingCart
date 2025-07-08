using AbySalto.Mid.Application.DTO;
using AbySalto.Mid.Application.Services.Interfaces;
using AbySalto.Mid.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(UserManager<User> userManager, IConfiguration configuration, IAuthService authService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto userDto)
        {
            var user = new User(userDto.Email, userDto.FirstName, userDto.LastName);
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully." });
            }
            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(new { Message = "User registration failed.", Errors = errors });

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userDto.Password))
                return Unauthorized("Invalid credentials");


            var accessToken = _authService.GenerateJwtToken(user);
            var refreshToken = _authService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                token = accessToken,
                refreshToken = refreshToken
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDto tokenRequest)
        {
            var principal = _authService.GetPrincipalFromExpiredToken(tokenRequest.AccessToken);
            if (principal == null) return BadRequest("Invalid access token.");

            var userId = principal.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || user.RefreshToken != tokenRequest.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Unauthorized("Invalid refresh token.");
            }

            var newAccessToken = _authService.GenerateJwtToken(user);
            var newRefreshToken = _authService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                token = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (userId == null)
                return Unauthorized("User not authenticated.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _userManager.UpdateAsync(user);

            return Ok(new { message = "Logged out successfully." });
        }

    }
}
