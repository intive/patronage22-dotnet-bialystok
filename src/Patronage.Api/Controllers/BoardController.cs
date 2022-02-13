using Microsoft.AspNetCore.Mvc;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Swashbuckle.AspNetCore.Annotations;

namespace Patronage.Api.Controllers
{
    [Route("api/board")]
    [ApiController]
    public class BoardController : Controller
    {
        private readonly IBoardService boardService;

        public BoardController(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        [SwaggerOperation(Summary = "Create Board.")]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult CreateBoard([FromBody] BoardDto filter)
        {
            var result = boardService.CreateBoard(filter);
            return Ok();
        }

        [SwaggerOperation(Summary = "Returns all Boards or filter Boards by Alias, Name, Description.")]
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<BoardDto>> GetBoards([FromQuery] FilterBoardDto? filter = null)
        {
            var boards = boardService.GetBoards(filter);
            return Ok(boards);
        }

        [SwaggerOperation(Summary = "Return Board by Id.")]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BoardDto> GetBoardById(int id)
        {
            var board = boardService.GetBoardById(id);
            if (board is null)
                return NotFound();

            return Ok(board);
        }

        [SwaggerOperation(Summary = "Full Board update. Expect complete Board's data.")]
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateBoard([FromBody] BoardDto boardDto)
        {
            var result = boardService.UpdateBoard(boardDto);
            return Ok();
        }

        [SwaggerOperation(Summary = "Updates Board - only selected properties")]
        [HttpPatch("updateLight")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateBoardLight([FromBody] PartialBoardDto boardDto)
        {
            var result = boardService.UpdateBoardLight(boardDto);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [SwaggerOperation(Summary = "Soft delete Board by Id")]
        [HttpPut("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteBoard(int id)
        {
            var result = boardService.DeleteBoard(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
