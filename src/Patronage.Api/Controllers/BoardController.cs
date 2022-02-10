using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.Boards.Commands.CreateBoard;
using Patronage.Api.MediatR.Boards.Commands.DeleteBoard;
using Patronage.Api.MediatR.Boards.Commands.LightUpdateBoard;
using Patronage.Api.MediatR.Boards.Commands.UpdateBoard;
using Patronage.Api.MediatR.Boards.Queries;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Swashbuckle.AspNetCore.Annotations;

namespace Patronage.Api.Controllers
{
    [Route("api/board")]
    [ApiController]
    public class BoardController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IBoardService boardService;

        public BoardController(IMediator mediator, IBoardService boardService)
        {
            _mediator = mediator;
            this.boardService = boardService;
        }




        [SwaggerOperation(Summary = "Create Board.")]
        [HttpPost("create")]
        public ActionResult<IEnumerable<BoardDto>> CreateBoard([FromBody] BoardDto filter)
        {
            var id = _mediator.Send(new CreateBoardCommand(filter));

            return Created($"/api/board/{id}", null);

            //return Ok(boardService.CreateBoard(filter));
        }




        [SwaggerOperation(Summary = "Returns all Boards or filter Boards by Alias, Name, Description.")]
        [HttpGet("list")]
        public ActionResult<IEnumerable<BoardDto>> GetBoards([FromBody] BoardDto filter)
        {
            var boards = _mediator.Send(new GetAllBoardsQuery(filter));

            return Ok(boards);


            //return Ok(boardService.GetBoards(filter));
        }




        [SwaggerOperation(Summary = "Return Board by Id.")]
        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<BoardDto>> GetBoardById(int id)
        {
            var board = _mediator.Send(new GetBoardByIdQuery(id));
            
            if (board is null) return NotFound();
            
            return Ok(board);


            //var board = boardService.GetBoardById(id);
            //if (board is null)
            //    return NotFound();

            //return Ok(board);
        }




        [SwaggerOperation(Summary = "Full Board update. Expect complete Board's data.")]
        [HttpPut("update")]
        public ActionResult<IEnumerable<BoardDto>> UpdateBoard([FromBody] BoardDto boardDto)
        {
            var result = _mediator.Send(new UpdateBoardCommand(boardDto));

            return Ok(result);


            //var result = boardService.UpdateBoard(boardDto);
            //return Ok(result);
        }




        [SwaggerOperation(Summary = "Light Board Update")]
        [HttpPatch("updateLight")]
        public ActionResult<IEnumerable<BoardDto>> UpdateBoardLight([FromBody] BoardDto boardDto)
        {
            var result = _mediator.Send(new LightUpdateBoardCommand(boardDto));

            return Ok(result);


            //var result = boardService.UpdateBoard(boardDto);
            //return Ok(result);
        }




        [SwaggerOperation(Summary = "Soft delete Board by Id")]
        [HttpDelete("delete/{id:int}")]
        public ActionResult<IEnumerable<BoardDto>> DeleteBoard(int id)
        {
            var result = _mediator.Send(new DeleteBoardCommand(id));

            return NoContent();


            //var result = boardService.DeleteBoard(id);
            //if (!result)
            //{
            //    return NotFound();
            //}
            //return Ok(result);
        }
    }
}
