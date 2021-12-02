using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Model;
using Microsoft.Extensions.Logging;
using RestWithASPNETUdemy.Repository;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")] 
    public class BookController : ControllerBase
    {

        #region INJECTION
        private IBookBusiness _bookBusiness;
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }
        #endregion

        #region GET
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookBusiness.FindById(id);

            if (book == null) return NotFound();

            return Ok(book);
    
        }
        #endregion

        #region POST
        [HttpPost]
        public IActionResult Post([FromForm] Book book)
        {
            if (book == null) return BadRequest();

            return Ok(_bookBusiness.Create(book));
        }
        #endregion

        #region PUT
        [HttpPut]
        public IActionResult Put([FromBody] Book book)
        {
            if (book == null) return BadRequest();

            return Ok(_bookBusiness.Update(book));
        }
        #endregion

        #region DELETE
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NotFound();
        }
        #endregion
    }
}
