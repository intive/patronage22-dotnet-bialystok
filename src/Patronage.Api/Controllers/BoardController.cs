using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.Board.Commands.Create;
using Patronage.Api.MediatR.Board.Commands.Delete;
using Patronage.Api.MediatR.Board.Commands.Update;
using Patronage.Api.MediatR.Board.Commands.UpdateLight;
using Patronage.Api.MediatR.Board.Queries.GetAll;
using Patronage.Api.MediatR.Board.Queries.GetSingle;
using Patronage.Contracts.ModelDtos.Board;
using Patronage.DataAccess;

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

        /// <summary>
        /// Create new Board based on object passed in request's body.
        /// </summary>
        /// <param name="boardDto">JSON object with properties defining a board to create</param>
        /// <response code="201">Board was created successfully.</response>
        /// <returns>Created board or null if board could not be created.</returns>
        [HttpPost("create")]
        public async Task<ActionResult<BaseResponse<BoardDto>>> CreateBoard([FromBody] CreateBoardCommand boardDto)
        {
            var result = await mediator.Send(boardDto);

            return CreatedAtAction(nameof(CreateBoard), new BaseResponse<BoardDto>
            {
                ResponseCode = StatusCodes.Status201Created,
                Message = "Board was created successfully.",
                Data = result
            });
        }

        /// <summary>
        /// Returns all Boards or filter board's collection by Alias, Name and Description.
        /// E.g if Alias and Name are present method return boards with the requested Alias and Name.
        /// </summary>
        /// <param name="filter">Object created from query containing Alias, Name and Descripion.</param>
        /// <response code="200">Boards were fetched successfully.</response>
        /// <response code="404">There's no boards.</response>
        /// <returns>Return all boards.</returns>
        [HttpGet("list")]
        public async Task<ActionResult<BaseResponse<IEnumerable<BoardDto>>>> GetBoards([FromQuery] FilterBoardDto filter)
        {
            var query = new GetBoardsQuery(filter);

            var result = await mediator.Send(query);

            if (result is null)
            {
                return NotFound(new BaseResponse<IEnumerable<BoardDto>>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = "There's no boards."
                });
            }

            return Ok(new BaseResponse<IEnumerable<BoardDto>>
            {
                ResponseCode = StatusCodes.Status200OK,
                Message = "Boards was fetched successfully.",
                Data = result
            });
        }

        /// <summary>
        /// Return Board by Id.
        /// </summary>
        /// <param name="id">Board's id.</param>
        /// <response code="200">Board was fetched successfully.</response>
        /// <response code="404">There's no board with requested Id.</response>
        /// <returns>Board with specified id.</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BaseResponse<BoardDto>>> GetBoardById(int id)
        {
            var query = new GetBoardByIdQuery(id);

            var result = await mediator.Send(query);

            if (result is null)
            {
                return NotFound(new BaseResponse<BoardDto>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no board with Id: {id}"
                });
            }

            return Ok(new BaseResponse<BoardDto>
            {
                ResponseCode = StatusCodes.Status200OK,
                Message = "Board was fetched successfully.",
                Data = result
            });
        }

        /// <summary>
        /// Full Board update. Expect complete Board's data.
        /// </summary>
        /// <param name="boardDto"></param>
        /// <param name="id">Board's id.</param>
        /// <response code="200">Board was updated successfully.</response>
        /// <response code="404">There's no board with requested Id.</response>
        /// <returns>True if board was updated successfully.</returns>
        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<BaseResponse<bool>>> UpdateBoard([FromBody] UpdateBoardDto boardDto, [FromRoute] int id)
        {
            var result = await mediator.Send(new UpdateBoardCommand
            {
                Data = boardDto,
                Id = id
            });

            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no board with Id: {id}",
                    Data = false
                });
            }

            return Ok(new BaseResponse<bool>
            {
                Message = "Board was updated successfully.",
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }

        /// <summary>
        /// Updates Board - only selected properties such as Alias, Name and Description.
        /// </summary>
        /// <param name="boardDto"></param>
        /// <param name="id">Board's id.</param>
        /// <response code="200">Board was updated successfully.</response>
        /// <response code="404">There's no board with requested Id.</response>
        /// <returns>True if board was updated successfully.</returns>
        [HttpPatch("updateLight/{id:int}")]
        public async Task<ActionResult<BaseResponse<bool>>> UpdateBoardLight([FromBody] PartialBoardDto boardDto, [FromRoute] int id)
        {
            var result = await mediator.Send(new UpdateBoardLightCommand
            {
                Data = boardDto,
                Id = id
            });

            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no board with Id: {id}",
                    Data = false
                });
            }

            return Ok(new BaseResponse<bool>
            {
                Message = "Board was updated successfully",
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }

        /// <summary>
        /// Soft delete Board with specified Id
        /// </summary>
        /// <param name="id">Board's id</param>
        /// <response code="200">Board was deleted successfully.</response>
        /// <response code="404">There's no board with requested Id.</response>
        /// <returns>True if Board was deleted successfully.</returns>
        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<BaseResponse<bool>>> DeleteBoard(int id)
        {
            var result = await mediator.Send(new DeleteBoardCommand { Id = id });

            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no board with Id: {id}",
                    Data = false
                });
            }

            return Ok(new BaseResponse<bool>
            {
                Message = "Board was deleted successfully",
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }
    }
}