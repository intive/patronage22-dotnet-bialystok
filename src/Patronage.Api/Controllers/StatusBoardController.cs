using Microsoft.AspNetCore.Mvc;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Swashbuckle.AspNetCore.Annotations;

namespace Patronage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusBoardController : ControllerBase
    {
        private readonly IBoardStatusService _boardStatusService;

        public StatusBoardController(IBoardStatusService boardStatusService )
        {
            _boardStatusService = boardStatusService;
        }


        [SwaggerOperation(Summary = "get all StatusBoards")]
        [HttpGet]
        public ActionResult<IEnumerable<BoardStatusDto>> GetAll()
        {
            var boards = _boardStatusService.GetAll();
            return Ok(boards);

        }


        [SwaggerOperation(Summary = "get StatusBoard by boardId, statusId")]
        [HttpGet("board")]
        public ActionResult<IEnumerable<BoardStatusDto>> GetById([FromQuery] int boardId,[FromQuery] int statusId)
        {
            var boards = _boardStatusService.GetById(boardId, statusId);
            return Ok(boards);

        }

        //[SwaggerOperation(Summary = "get StatusBoards for boardid")]
        //[HttpGet]


        [SwaggerOperation(Summary = "create StatusBoard based on statusboardDto passed as parameter in request body")]
        [HttpPost]
        public ActionResult Create([FromBody] BoardStatusDto dto)
        {
            int id = _boardStatusService.Create(dto);
            return Created($"{id}", null);
        }


        [SwaggerOperation(Summary = "delete statusboard by id")]
        [HttpDelete]
        public void Delete([FromQuery] int boardId, [FromQuery] int statusId)
        {
            _boardStatusService.Delete(boardId, statusId);

        }
    }
}
