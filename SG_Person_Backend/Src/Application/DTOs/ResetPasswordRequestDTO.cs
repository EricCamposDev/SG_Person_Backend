namespace SG_Person_Backend.Src.Application.DTOs
{
    public class ResetPasswordRequestDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
