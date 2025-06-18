using System.ComponentModel.DataAnnotations;

namespace SG_Person_Backend.Src.Application.DTOs
{
    public class PersonCreateDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres")]
        public string Name { get; set; } = null!;  // Non-nullable para criação

        [Required(ErrorMessage = "O gênero é obrigatório")]
        [StringLength(20)]
        public string Gender { get; set; } = null!;

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email em formato inválido")]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [StringLength(100, ErrorMessage = "O local de nascimento não pode exceder 100 caracteres")]
        public string? BirthPlace { get; set; }

        [StringLength(50, ErrorMessage = "A nacionalidade não pode exceder 50 caracteres")]
        public string? Nationality { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "CPF deve ter entre 11 e 14 caracteres")]
        [RegularExpression(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$", ErrorMessage = "CPF em formato inválido")]
        public string Cpf { get; set; } = null!;
    }
}