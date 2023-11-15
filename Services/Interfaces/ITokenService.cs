using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Webcomic.Models.Entities;

namespace Webcomic.Services.Interfaces
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> GenerateAccessToken(AppUser user, IEnumerable<string> userRoles);
        Task<string> GenerateRefreshToken();
        Task<ClaimsPrincipal?> GetPrincipalFromExpiredToken(string? token);
    }
}
