using Microsoft.AspNetCore.Mvc;
using SG_Person_Backend.Src.Application.DTOs;

namespace SG_Person_Backend.Src.Application.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDTO>> GetAllAsync();

        //Task<ActionResult<PersonDTO>> GetById(int Id);

        //Task<ActionResult<PersonDTO>> CreateAsync([FromBody] PersonCreateDTO dto);
    }
}
