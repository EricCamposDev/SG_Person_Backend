using System.ComponentModel.DataAnnotations;

namespace SG_Person_Backend.Src.Application.DTOs
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "Username é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username deve ter entre 3 e 50 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Senha deve ter no mínimo 8 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmação de senha é obrigatória")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}