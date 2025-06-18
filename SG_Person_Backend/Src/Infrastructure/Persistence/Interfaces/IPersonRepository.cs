using SG_Person_Backend.Src.Domain.Entities;

namespace SG_Person_Backend.Src.Infrastructure.Persistence.Interfaces
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();
    }
}
