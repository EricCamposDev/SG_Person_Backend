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

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            _secretKey = _configuration["Jwt:Secret"] ?? throw new ArgumentNullException("Jwt:Secret", "Chave secreta não configurada");
        }

        public string GenerateToken(User user)
        {
            // Validação completa do parâmetro user e suas propriedades
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Username não pode ser nulo ou vazio", nameof(user.Username));

            if (string.IsNullOrWhiteSpace(user.Role))
                throw new ArgumentException("Role não pode ser nulo ou vazio", nameof(user.Role));

            // Criação segura das claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString() ?? string.Empty),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
