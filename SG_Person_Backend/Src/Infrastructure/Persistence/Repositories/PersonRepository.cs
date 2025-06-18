using Microsoft.EntityFrameworkCore;
using SG_Person_Backend.Src.Domain.Entities;
using SG_Person_Backend.Src.Infrastructure.Persistence.Interfaces;

namespace SG_Person_Backend.Src.Infrastructure.Persistence.Repositories
{
    public class PersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Person.ToListAsync();
        }
    }
}
