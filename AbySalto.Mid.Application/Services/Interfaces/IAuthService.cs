using AbySalto.Mid.Domain.Entities;
using System.Security.Claims;

namespace AbySalto.Mid.Application.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
