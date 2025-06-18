using SG_Person_Backend.Src.Application.Interfaces;
using BCrypt.Net;

namespace SG_Person_Backend.Src.Infrastructure.Services
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        private readonly HashType _hashType;
        private readonly int _workFactor;

        public BCryptPasswordHasher(int workFactor = 12)
        {
            _workFactor = workFactor;
            _hashType = HashType.SHA256; // Algoritmo mais seguro
        }

        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: _workFactor);

        }

        public bool Verify(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword, hashType: _hashType);
        }
    }
}
