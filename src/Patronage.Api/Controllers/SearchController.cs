using Microsoft.AspNetCore.Mvc;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.DataAccess;

namespace Patronage.Api.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : Controller
    {
        private readonly ILuceneService _luceneService;

        public SearchController(ILuceneService luceneService)
        {
            _luceneService = luceneService;
        }

        /// <summary>
        /// Perform full text search on Boards, Projects and Issues.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns>Object with 3 properties - list of filtered projects, boards and issue. If there's no match list is set to null.</returns>
        [HttpGet]
        public ActionResult Search([FromQuery] string? name, string? description)
        {
            var result = _luceneService.Search(name, description);

            var message = "Query was executed successfully.";

            if (result.Issues is null && result.Boards is null && result.Projects is null)
            {
                message = "There's no entities that match.";
            }

            return Ok(new BaseResponse<FilteredEntities>
            {
                ResponseCode = StatusCodes.Status200OK,
                Message = message,
                Data = result
            });
        }
    }
}