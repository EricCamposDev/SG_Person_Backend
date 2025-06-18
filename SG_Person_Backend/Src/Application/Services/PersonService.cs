using AutoMapper;
using SG_Person_Backend.Src.Application.DTOs;
using SG_Person_Backend.Src.Application.Interfaces;
using SG_Person_Backend.Src.Infrastructure.Persistence.Interfaces;

namespace SG_Person_Backend.Src.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            var persons =  await _personRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PersonDTO>>(persons);
        }


    }
}
