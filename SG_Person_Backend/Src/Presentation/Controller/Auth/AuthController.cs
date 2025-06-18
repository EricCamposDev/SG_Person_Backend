//using Microsoft.AspNetCore.Mvc;
//using SG_Person_Backend.Src.Application.Services;

//namespace SG_Person_Backend.Src.Presentation.Controller.Auth
//{
//    [ApiController]
//    [Route("api/auth")]
//    public class AuthController : ControllerBase
//    {
//        private readonly AuthService _authService;

//        public AuthController(AuthService authService)
//        {
//            _authService = authService;
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginRequest request)
//        {
//            var result = await _authService.AuthenticateAsync(request.Username, request.Password);

//            if (!result.Success)
//                return Unauthorized(result.Error);

//            return Ok(new { Token = result.Token });
//        }
//    }

//    public record LoginRequest(string Username, string Password);
//}
