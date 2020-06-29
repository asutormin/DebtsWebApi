using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DebtsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiWorkingController : ControllerBase
    {
        private readonly ILogger<ApiWorkingController> _logger;

        public ApiWorkingController(ILogger<ApiWorkingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("DebtsWebApi работает!");
        }
    }
}
