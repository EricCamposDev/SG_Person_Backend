using SG_Person_Backend.Src.Domain.Common;
//using SG_Person_Backend.Src.Domain.Entities;

namespace SG_Person_Backend.Src.Domain.Entities
{
    public class Person : AuditableEntity
    {
        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public string? Gender { get; private set; }
        public string? Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string? BirthPlace { get; private set; }
        public string? Nationality { get; private set; }
        public string? Cpf { get; private set; }

        //public Address? Address { get; private set; } // Para V2

        //protected Person() {} // Para EF Core

        public static Person Create(
            string name,
            string cpf,
            DateTime birthDate,
            string? gender = null,
            string? email = null,
            string? birthPlace = null,
            string? nationality = null)
        {
            //if (string.IsNullOrWhiteSpace(name))
            //    throw new CannotUnloadAppDomainException("Nome é obrigatório");

            //if (!CpfValidator.IsValid(cpf))
            //    throw new CannotUnloadAppDomainException("CPF inválido");

            //if (birthDate > DateTime.Now.AddYears(-5) || birthDate < DateTime.Now.AddYears(-120))
            //    throw new CannotUnloadAppDomainException("Data de nascimento inválida");

            //if (!string.IsNullOrEmpty(email) && !EmailValidator.IsValid(email))
            //    throw new CannotUnloadAppDomainException("E-mail inválido");

            return new Person
            {
                Name = name,
                Cpf = cpf,
                BirthDate = birthDate,
                Gender = gender,
                Email = email,
                BirthPlace = birthPlace,
                Nationality = nationality
            };
        }

        //public void UpdateAddress(string street, string city, string state, string zipCode)
        //{
        //    Address = Address.Create(street, city, state, zipCode);
        //}
    }
}
