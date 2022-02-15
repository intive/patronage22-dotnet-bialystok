using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.Board.Commands.Create;
using Patronage.Api.MediatR.Board.Commands.Delete;
using Patronage.Api.MediatR.Board.Commands.Update;
using Patronage.Api.MediatR.Board.Commands.UpdateLight;
using Patronage.Api.MediatR.Board.Queries.GetAll;
using Patronage.Api.MediatR.Board.Queries.GetSingle;
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
        public async Task<ActionResult> CreateBoard([FromBody] CreateBoardCommand boardDto)
        {
            var result = await mediator.Send(boardDto);

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
                Data = result
            });
        }

        [SwaggerOperation(Summary = "Returns all Boards or filter Boards by Alias, Name, Description.")]
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseResponse<IEnumerable<BoardDto>>>> GetBoards([FromQuery] FilterBoardDto filter)
        {
            var query = new GetBoardsQuery(filter);

            var result = await mediator.Send(query);

            if (result is null)
            {
                return NotFound(new BaseResponse<IEnumerable<BoardDto>>
                {
                    ResponseCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(new BaseResponse<IEnumerable<BoardDto>>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }

        [SwaggerOperation(Summary = "Return Board by Id.")]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BoardDto>> GetBoardById(int id)
        {
            var query = new GetBoardByIdQuery(id);

            var result = await mediator.Send(query);

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
                Data = result
            });
        }

        [SwaggerOperation(Summary = "Full Board update. Expect complete Board's data.")]
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateBoard([FromBody] UpdateBoardCommand boardDto)
        {
            var result = await mediator.Send(boardDto);

            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }

        [SwaggerOperation(Summary = "Updates Board - only selected properties")]
        [HttpPatch("updateLight")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateBoardLight([FromBody] UpdateBoardLightCommand boardDto)
        {
            var result = await mediator.Send(boardDto);

            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }

        [SwaggerOperation(Summary = "Soft delete Board by Id")]
        [HttpPut("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteBoard(int id)
        {
            var result = await mediator.Send(new DeleteBoardCommand { Id = id });

            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }
    }
}
