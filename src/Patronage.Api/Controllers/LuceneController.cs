using Microsoft.AspNetCore.Mvc;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.DataAccess;

namespace Patronage.Api.Controllers
{
    [Route("api/filter")]
    [ApiController]
    public class LuceneController : Controller
    {
        private readonly ILuceneService _luceneService;

        public LuceneController(ILuceneService luceneService)
        {
            _luceneService = luceneService;
        }

        [HttpGet]
        public ActionResult Search([FromQuery] string name = "Tutaj jestem", [FromQuery] string description = "Tutaj jestem")
        {
            var result = _luceneService.Search(name, description);

            return Ok(new BaseResponse<FilteredEntities>
            {
                ResponseCode = StatusCodes.Status200OK,
                Message = "Query was executed successfully.",
                Data = result
            });
        }
    }
}