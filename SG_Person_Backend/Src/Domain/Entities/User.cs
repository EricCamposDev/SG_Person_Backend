using SG_Person_Backend.Src.Domain.Common;
using System.Text.RegularExpressions;

namespace SG_Person_Backend.Src.Domain.Entities
{
    public class User : AuditableEntity
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; } // Alterado para PasswordHash
        public bool IsActive { get; private set; } // Removido nullable

        // Construtor privado para EF Core
        private User() { }

        // Factory method
        public static User Create(
            string username,
            string email,
            string passwordHash) // Recebe o hash pronto
        {
            Validate(username, email);

            return new User
            {
                Id = Guid.NewGuid(),
                Username = username.Trim(),
                Email = email.ToLower().Trim(),
                PasswordHash = passwordHash,
                IsActive = true
            };
        }

        private static void Validate(string username, string email)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username é obrigatório");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email é obrigatório");

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Email inválido");
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}