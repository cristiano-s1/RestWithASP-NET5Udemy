using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RestWithASPNETUdemy.Data.VO;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Business;
using Microsoft.AspNetCore.Authorization;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")] //Validar autenticação
    [Route("api/[controller]/v{version:apiVersion}")]
    //[Route("api/[controller]")]
    public class PersonController : ControllerBase
    {

        #region INJECTION
        private readonly ILogger<PersonController> _logger;

        // Declaration of the service used
        private IPersonBusiness _personBusiness;   

        // Injection of an instance of IPersonService
        // when creating an instance of PersonController
        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }
        #endregion

        #region GET
        // Maps GET requests to https://localhost:{port}/api/person
        // Get no parameters for FindAll -> Search All
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))] //Swagger tipo de retorno
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((4001))]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }


        // Maps GET requests to https://localhost:{port}/api/person/{id}
        // receiving an ID as in the Request Path
        // Get with parameters for FindById -> Search by ID
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))] //Swagger tipo de retorno
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((4001))]
        public IActionResult Get(int id)
        {
            var person = _personBusiness.FindById(id);

            if (person == null) return NotFound();

            return Ok(person);
        }
        #endregion

        #region POST
        // Maps POST requests to https://localhost:{port}/api/person/
        // [FromBody] consumes the JSON object sent in the request body
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(PersonVO))] //Swagger tipo de retorno
        [ProducesResponseType((400))]
        [ProducesResponseType((4001))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();

            return Ok(_personBusiness.Create(person));
        }
        #endregion

        #region PUT
        // Maps PUT requests to https://localhost:{port}/api/person/
        // [FromBody] consumes the JSON object sent in the request body
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(PersonVO))] //Swagger tipo de retorno
        [ProducesResponseType((400))]
        [ProducesResponseType((4001))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();

            return Ok(_personBusiness.Update(person));
        }
        #endregion

        #region PATCH
        [HttpPatch("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))] //Swagger tipo de retorno
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((4001))]
        public IActionResult Patch(int id)
        {
            var person = _personBusiness.Disable(id);

            return Ok(person);
        }
        #endregion

        #region DELETE
        // Maps DELETE requests to https://localhost:{port}/api/person/{id}
        // receiving an ID as in the Request Path
        [HttpDelete("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))] //Swagger tipo de retorno
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((4001))]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);

            return NoContent();
        }
        #endregion
    }
}
