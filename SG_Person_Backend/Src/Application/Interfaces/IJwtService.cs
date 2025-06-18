using SG_Person_Backend.Src.Domain.Entities;

namespace SG_Person_Backend.Src.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
