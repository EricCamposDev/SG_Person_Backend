namespace SG_Person_Backend.Src.Application.DTOs
{
    public class PersonDTO
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string? BirthPlace { get; set; }
        public string? Nationality { get; set; }
        public string? Cpf { get; set; }
    }
}
