using SG_Person_Backend.Src.Domain.Common;

namespace SG_Person_Backend.Src.Domain.Entities
{
    public class User : AuditableEntity
    {
        public Guid? Id { get; private set; }
        public string? Username { get; private set; }
        public string? Email { get; private set; }
        public string? PasswordHash { get; private set; }
        public string? Role { get; private set; }
        public bool? IsActive { get; private set; }

        // Construtor privado para EF Core
        protected User() {
        }

        // Factory method
        public static User Create(
            string username,
            string email,
            string passwordHash,
            string role)
        {
            Validate(username, email, role);

            return new User
            {
                Id = Guid.NewGuid(),
                Username = username.Trim(),
                Email = email.ToLower().Trim(),
                PasswordHash = passwordHash,
                Role = role,
                IsActive = true
            };
        }

        private static void Validate(string username, string email, string role)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new CannotUnloadAppDomainException("Username é obrigatório");

            if (string.IsNullOrWhiteSpace(email))
                throw new CannotUnloadAppDomainException("Email é obrigatório");

            //if (!EmailValidator.IsValid(email))
            //    throw new CannotUnloadAppDomainException("Email inválido");

            if (string.IsNullOrWhiteSpace(role))
                throw new CannotUnloadAppDomainException("Role é obrigatória");
        }

        public void UpdateRole(string newRole)
        {
            Role = newRole;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
