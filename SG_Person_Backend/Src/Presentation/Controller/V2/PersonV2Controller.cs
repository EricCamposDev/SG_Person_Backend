//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace SG_Person_Backend.Src.Presentation.v2
//{
//    [ApiController]
//    [Route("api/v2/[controller]")]
//    [Authorize]
//    public class PeopleV2Controller : ControllerBase
//    {
//        private readonly IPersonService _personService;

//        [HttpPost]
//        public async Task<ActionResult<PersonDto>> Create([FromBody] PersonCreateV2Dto dto)
//        {
//            if (dto.Address == null)
//                return BadRequest("Endereço é obrigatório na versão 2");

//            var result = await _personService.CreateWithAddressAsync(dto);
//            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
//        }
//    }
//}
