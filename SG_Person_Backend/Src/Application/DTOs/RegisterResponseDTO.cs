namespace SG_Person_Backend.Src.Application.DTOs
{
    public class RegisterResponseDTO
    {
        public string UserId { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
