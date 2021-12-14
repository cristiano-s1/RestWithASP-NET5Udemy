using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RestWithASPNETUdemy.Data.VO;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Repository;
using Microsoft.AspNetCore.Authorization;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")] //Validar autenticação
    [Route("api/[controller]/v{version:apiVersion}")]
    //[Route("api/[controller]")]
    public class BookController : ControllerBase
    {

        #region INJECTION
        private readonly ILogger<BookController> _logger;

        private IBookBusiness _bookBusiness;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }
        #endregion

        #region GET
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))] //Swagger tipo de retorno
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((4001))]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(BookVO))] //Swagger tipo de retorno
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((4001))]
        public IActionResult Get(int id)
        {
            var book = _bookBusiness.FindById(id);

            if (book == null) return NotFound();

            return Ok(book);

        }
        #endregion

        #region POST
        [HttpPost]
        [ProducesResponseType((200), Type = typeof(BookVO))] //Swagger tipo de retorno
        [ProducesResponseType((400))]
        [ProducesResponseType((4001))]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();

            return Ok(_bookBusiness.Create(book));
        }
        #endregion

        #region PUT
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(BookVO))] //Swagger tipo de retorno
        [ProducesResponseType((400))]
        [ProducesResponseType((4001))]
        public IActionResult Put([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();

            return Ok(_bookBusiness.Update(book));
        }
        #endregion

        #region DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType((200), Type = typeof(BookVO))] //Swagger tipo de retorno
        [ProducesResponseType((204))]
        [ProducesResponseType((400))]
        [ProducesResponseType((4001))]
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);

            return NoContent();
        }
        #endregion
    }
}
