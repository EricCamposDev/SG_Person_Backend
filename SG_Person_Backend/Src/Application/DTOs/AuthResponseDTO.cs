namespace SG_Person_Backend.Src.Application.DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
        public UserInfoDTO UserInfo { get; set; }
    }
}
