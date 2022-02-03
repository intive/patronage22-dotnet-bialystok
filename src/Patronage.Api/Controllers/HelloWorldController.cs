using Microsoft.AspNetCore.Mvc;

namespace Patronage.Api.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
    private readonly ILogger<HelloWorldController> _logger;

    public HelloWorldController(ILogger<HelloWorldController> logger)
    {
        _logger = logger;
    }


        // GET: api/<HelloWorldController>
        [HttpGet]
        public string HelloWorld()
        {
            _logger.LogInformation("hello world");
            return "Hello World";
        }
    }
}
