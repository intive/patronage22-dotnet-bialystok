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
        public ActionResult<IEnumerable<BoardDto>> CreateBoard([FromBody] BoardDto filter)
        {
            return Ok(boardService.CreateBoard(filter));
        }

        [SwaggerOperation(Summary = "Returns all Boards or filter Boards by Alias, Name, Description.")]
        [HttpGet("list")]
        public ActionResult<IEnumerable<BoardDto>> GetBoards([FromBody] BoardDto filter)
        {
            return Ok(boardService.GetBoards(filter));
        }

        [SwaggerOperation(Summary = "Return Board by Id.")]
        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<BoardDto>> GetBoardById(int id)
        {
            var board = boardService.GetBoardById(id);
            if (board is null)
                return NotFound();

            return Ok(board);
        }

        [SwaggerOperation(Summary = "Full Board update. Expect complete Board's data.")]
        [HttpPut("update")]
        public ActionResult<IEnumerable<BoardDto>> UpdateBoard([FromBody] BoardDto boardDto)
        {
            var result = boardService.UpdateBoard(boardDto);
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Light Board Update")]
        [HttpPut("updateLight")]
        public ActionResult<IEnumerable<BoardDto>> UpdateBoardLight([FromBody] BoardDto boardDto)
        {
            var result = boardService.UpdateBoard(boardDto);
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Soft delete Board by Id")]
        [HttpPut("delete/{id:int}")]
        public ActionResult<IEnumerable<BoardDto>> DeleteBoard(int id)
        {
            var result = boardService.DeleteBoard(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
