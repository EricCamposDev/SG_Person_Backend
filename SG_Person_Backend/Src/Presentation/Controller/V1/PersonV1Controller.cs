using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SG_Person_Backend.Src.Application.DTOs;
using SG_Person_Backend.Src.Application.Interfaces;

namespace SG_Person_Backend.Src.Presentation.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class PersonV1Controller : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonV1Controller(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetAll()
        {
            var persons = await _personService.GetAllAsync();
            return Ok(persons);
        }

        //[HttpPost]
        //public async Task<ActionResult<PersonDTO>> Create([FromBody] PersonCreateDTO personDTO)
        //{
        //    var result = await _personService.CreateAsync(personDTO);

        //    return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        //}
    }
}
