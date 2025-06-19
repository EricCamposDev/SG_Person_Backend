using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using SG_Person_Backend.Src.Application.DTOs;
using SG_Person_Backend.Src.Application.Interfaces;
using SG_Person_Backend.Src.Infrastructure.Identity;

namespace SG_Person_Backend.Src.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            IEmailService emailService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public async Task<AuthResponseDTO> AuthenticateAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new UnauthorizedAccessException("Usuário não encontrado");

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!passwordValid)
                throw new UnauthorizedAccessException("Credenciais inválidas");

            var token = await _tokenService.GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponseDTO
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddDays(7),
                RefreshToken = refreshToken.Token,
                UserInfo = new UserInfoDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.Username
                }
            };
        }

        public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO request)
        {
            if (request.Password != request.ConfirmPassword)
                throw new ArgumentException("As senhas não coincidem");

            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
                throw new ArgumentException("Email já está em uso");

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Username
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new RegisterResponseDTO
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            // Atribuir role padrão
            //await _userManager.AddToRoleAsync(user, "Basic");

            return new RegisterResponseDTO
            {
                UserId = user.Id,
                Success = true
            };
        }

        public async Task<AuthResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO request)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.Token);
            var userId = principal.Claims.First(c => c.Type == "nameid").Value;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new SecurityTokenException("Token inválido");

            var storedRefreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token == request.RefreshToken);
            if (storedRefreshToken == null || storedRefreshToken.IsExpired)
                throw new SecurityTokenException("Refresh token inválido ou expirado");

            var newToken = await _tokenService.GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken();

            // Revogar o token antigo
            storedRefreshToken.Revoked = DateTime.UtcNow;
            user.RefreshTokens.Add(newRefreshToken);

            await _userManager.UpdateAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponseDTO
            {
                Token = newToken,
                Expiration = DateTime.UtcNow.AddDays(7),
                RefreshToken = newRefreshToken.Token,
                UserInfo = new UserInfoDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Username = user.Username
                }
            };
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.UtcNow.AddDays(10),
                Created = DateTime.UtcNow
            };
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return true; // Não revelar que o usuário não existe

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Implementar envio de email aqui
            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequestDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new ArgumentException("Usuário não encontrado");

            if (request.NewPassword != request.ConfirmPassword)
                throw new ArgumentException("As senhas não coincidem");

            var result = await _userManager.ResetPasswordAsync(
                user, request.Token, request.NewPassword);

            if (!result.Succeeded)
                throw new ApplicationException(string.Join(", ", result.Errors.Select(e => e.Description)));

            return true;
        }
    }
}
