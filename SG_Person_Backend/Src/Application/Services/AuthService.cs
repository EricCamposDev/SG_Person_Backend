using Microsoft.AspNetCore.Identity;
using SG_Person_Backend.Src.Application.DTOs;
using SG_Person_Backend.Src.Application.Interfaces;
using SG_Person_Backend.Src.Domain.Entities;
using SG_Person_Backend.Src.Infrastructure.Persistence.Interfaces;

namespace SG_Person_Backend.Src.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(
            IUserRepository userRepository,
            IJwtService jwtService,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResult> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if( user == null )
            {
                return AuthResult.CreateFailure("Usuário não cadastrado");
            }

            if ( user.IsActive == false)
            {
                return AuthResult.CreateFailure("Usuário inativo.");
            }


            if (!_passwordHasher.Verify(password, user.PasswordHash))
            {
                return AuthResult.CreateFailure("Credenciais inválidas");
            }

            var token = _jwtService.GenerateToken(user);
            return AuthResult.CreateSuccess(token);
        }

        public async Task<AuthResult> RegisterAsync(RegisterRequestDTO request)
        {
            var username = await _userRepository.GetByUsernameAsync(request.Username);
            if (username != null)
            {
                return AuthResult.CreateFailure("Usuário já cadastrado.");
            }

            var email = await _userRepository.GetByEmailAsync(request.Email);
            if( email != null)
            {
                return AuthResult.CreateFailure("Email já cadastrado.");
            }

            var passwordHash = _passwordHasher.Hash(request.Password);

            var user = User.Create(
                request.Username,
                request.Email,
                passwordHash);

            await _userRepository.AddAsync(user);

            return AuthResult.CreateSuccess(user.Username);

        }
    }

    public record AuthResult(bool Success, string? Token, string? Error)
    {
        public static AuthResult CreateSuccess(string token) => new(true, token, null);
        public static AuthResult CreateFailure(string error) => new(false, null, error);
    }
}
