using SG_Person_Backend.Src.Application.DTOs;
using SG_Person_Backend.Src.Application.Services;

namespace SG_Person_Backend.Src.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> AuthenticateAsync(string username, string password);
        Task<AuthResult> RegisterAsync(RegisterRequestDTO request);
    }
}
