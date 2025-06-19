using Microsoft.AspNetCore.Identity.Data;
using SG_Person_Backend.Src.Application.DTOs;
using System.Threading.Tasks;

namespace SG_Person_Backend.Src.Application.Interfaces
{
    public interface IAccountService
    {
        Task<AuthResponseDTO> AuthenticateAsync(LoginRequest request);
        Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO request);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordRequestDTO request);
        //Task<bool> VerifyEmailAsync(string token);
    }
}
