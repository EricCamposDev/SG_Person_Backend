using Microsoft.IdentityModel.Tokens;
using SG_Person_Backend.Src.Application.Interfaces;
using SG_Person_Backend.Src.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SG_Person_Backend.Src.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiryInMinutes;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            _secretKey = _configuration["Jwt:Secret"] ?? throw new ArgumentNullException("Jwt:Secret", "Chave secreta não configurada");
            _issuer = _configuration["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer", "Issuer não configurado");
            _audience = _configuration["Jwt:Audience"] ?? throw new ArgumentNullException("Jwt:Audience", "Audience não configurado");
            _expiryInMinutes = _configuration.GetValue<int>("Jwt:ExpiryInMinutes", 120); // Default 2 horas
        }

        public string GenerateToken(User user)
        {
            // Validação completa
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Username não pode ser nulo ou vazio", nameof(user.Username));

            if (user.Id == Guid.Empty)
                throw new ArgumentException("Id do usuário inválido", nameof(user.Id));

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
                // Adicione outras claims conforme necessário
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expiryInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}