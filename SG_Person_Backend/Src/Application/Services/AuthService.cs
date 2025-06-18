//using Microsoft.AspNetCore.Identity;
//using SG_Person_Backend.Src.Application.Interfaces;
//using SG_Person_Backend.Src.Infrastructure.Persistence.Interfaces;

//namespace SG_Person_Backend.Src.Application.Services
//{
//    public class AuthService
//    {
//        private readonly IUserRepository _userRepository;
//        private readonly IJwtService _jwtService;
//        private readonly IPasswordHasher _passwordHasher;

//        public AuthService(
//            IUserRepository userRepository,
//            IJwtService jwtService,
//            IPasswordHasher passwordHasher)
//        {
//            _userRepository = userRepository;
//            _jwtService = jwtService;
//            _passwordHasher = passwordHasher;
//        }

//        public async Task<AuthResult> AuthenticateAsync(string username, string password)
//        {
//            var user = await _userRepository.GetByUsernameAsync(username);

//            if (user == null || !user.IsActive ||
//                !_passwordHasher.Verify(password, user.PasswordHash))
//            {
//                return AuthResult.CreateFailure("Credenciais inválidas");
//            }

//            var token = _jwtService.GenerateToken(user);
//            return AuthResult.CreateSuccess(token);
//        }
//    }

//    public record AuthResult(bool Success, string? Token, string? Error)
//    {
//        public static AuthResult CreateSuccess(string token) => new(true, token, null);
//        public static AuthResult CreateFailure(string error) => new(false, null, error);
//    }
//}
