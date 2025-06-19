using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SG_Person_Backend.Src.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        public bool IsActive { get; set; } = true;
    }
}


public class RefreshToken
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Revoked { get; set; }

    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsActive => !IsExpired && Revoked == null;
}