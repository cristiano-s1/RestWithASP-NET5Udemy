using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        
        private readonly ILogger<PersonController> _logger;

        // Injection of an instance of IPersonService
        // when creating an instance of PersonController
        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

      
    }
}
