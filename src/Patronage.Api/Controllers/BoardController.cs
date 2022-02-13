using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.Commands;
using Patronage.Api.Queries;
using Patronage.Contracts.ModelDtos;
using Patronage.DataAccess;
using Swashbuckle.AspNetCore.Annotations;

namespace Patronage.Api.Controllers
{
    [Route("api/board")]
    [ApiController]
    public class BoardController : Controller
    {
        private readonly IMediator mediator;
        public BoardController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [SwaggerOperation(Summary = "Create Board.")]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult CreateBoard([FromBody] CreateBoardCommand boardDto)
        {
            var result = mediator.Send(boardDto);

            if (!result.Result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result.Result
            });
        }

        [SwaggerOperation(Summary = "Returns all Boards or filter Boards by Alias, Name, Description.")]
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BaseResponse<IEnumerable<BoardDto>>> GetBoards([FromQuery] FilterBoardDto filter)
        {
            var query = new GetBoardsQuery(filter);

            var result = mediator.Send(query);

            if(result is null)
            {
                return NotFound(new BaseResponse<IEnumerable<BoardDto>>{
                    ResponseCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(new BaseResponse<IEnumerable<BoardDto>>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result.Result             
            });
        }

        [SwaggerOperation(Summary = "Return Board by Id.")]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BoardDto> GetBoardById(int id)
        {
            var query = new GetBoardByIdQuery(id);

            var result = mediator.Send(query);

            if (result is null)
            {
                return NotFound(new BaseResponse<BoardDto>
                {
                    ResponseCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(new BaseResponse<BoardDto>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result.Result
            });
        }

        [SwaggerOperation(Summary = "Full Board update. Expect complete Board's data.")]
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateBoard([FromBody] UpdateBoardCommand boardDto)
        {
            var result = mediator.Send(boardDto);

            if (!result.Result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result.Result
            });
        }

        [SwaggerOperation(Summary = "Updates Board - only selected properties")]
        [HttpPatch("updateLight")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateBoardLight([FromBody] UpdateBoardLightCommand boardDto)
        {
            var result = mediator.Send(boardDto);

            if (!result.Result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result.Result
            });
        }

        [SwaggerOperation(Summary = "Soft delete Board by Id")]
        [HttpPut("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteBoard(int id)
        {
            var result = mediator.Send(new DeleteBoardCommand { Id = id });

            if (!result.Result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result.Result
            });
        }
    }
}
