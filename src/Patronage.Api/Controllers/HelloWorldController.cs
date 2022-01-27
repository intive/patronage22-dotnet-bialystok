using Microsoft.AspNetCore.Mvc;



namespace Patronage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        // GET: api/<HelloWorldController>
        [HttpGet]
        public string HelloWorld()
        {
            return "Hello World";
        }

    }
}
