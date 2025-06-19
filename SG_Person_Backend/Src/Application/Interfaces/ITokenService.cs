using SG_Person_Backend.Src.Infrastructure.Identity;
using System.Security.Claims;

namespace SG_Person_Backend.Src.Application.Interfaces
{
    public interface ITokenService
    {
        /// <summary>
        /// Generates a JWT token for the specified user
        /// </summary>
        /// <param name="user">The user to generate the token for</param>
        /// <returns>A signed JWT token string</returns>
        Task<string> GenerateJwtToken(ApplicationUser user);

        /// <summary>
        /// Generates a cryptographically secure refresh token
        /// </summary>
        /// <returns>A base64 encoded refresh token string</returns>
        Task<string> GenerateRefreshToken();

        /// <summary>
        /// Validates an expired JWT token and returns its principal
        /// </summary>
        /// <param name="token">The expired JWT token</param>
        /// <returns>The ClaimsPrincipal extracted from the token</returns>
        /// <exception cref="SecurityTokenException">Thrown when the token is invalid</exception>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
