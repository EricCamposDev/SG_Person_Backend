using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SG_Person_Backend.Src.Application.DTOs;
using SG_Person_Backend.Src.Application.Interfaces;
using SG_Person_Backend.Src.Domain.Entities;

namespace SG_Person_Backend.Src.Presentation.v1
{
    [ApiController]
    [Route("api/v1")]
    [Authorize]
    [ApiExplorerSettings(GroupName = "v1")]
    [Tags("Persons")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class PersonV1Controller : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonV1Controller(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("persons")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PersonDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
