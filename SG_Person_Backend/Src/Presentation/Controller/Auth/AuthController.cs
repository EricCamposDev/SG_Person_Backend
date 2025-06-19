using Microsoft.AspNetCore.Mvc;
using SG_Person_Backend.Src.Application.Services;
using SG_Person_Backend.Src.Application.DTOs;
using Swashbuckle.AspNetCore.Annotations;
using SG_Person_Backend.Src.Application.Interfaces;
using SG_Person_Backend.Src.Domain.Entities;

namespace SG_Person_Backend.Src.Presentation.Controller.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Autenticação de usuário e retorno de token JWT")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.AuthenticateAsync(request.Username, request.Password);

            if (!result.Success)
                return Unauthorized(result.Error);

            return Ok(new { Token = result.Token });
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(Summary = "Cadastro de usuário para acesso JWT")]
        public async Task<IActionResult> RegisterAsync(RegisterRequestDTO register)
        {
            var result = await _authService.RegisterAsync(register);
            if( !result.Success)
            {
                return Unauthorized(result.Error);
            }

            return Ok(new { User = register });
        }
    }

    public record LoginRequest(string Username, string Password);
}
