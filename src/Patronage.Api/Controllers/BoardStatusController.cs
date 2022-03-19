using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.BoardStatus.Commands;
using Patronage.Api.MediatR.BoardStatus.Queries;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.BoardsStatus;
using Patronage.DataAccess;
using Swashbuckle.AspNetCore.Annotations;

namespace Patronage.Api.Controllers
{
    [Route("api/boardStatus")]
    [ApiController]
    public class BoardStatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BoardStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all BoardStatus records available in the database.
        /// You can add BoardId, StatusId or both.
        /// </summary>
        /// <response code="200">Returning all records or all records matching from BoardStatus table.</response>
        /// <response code="404">No BoardStatus found in the database.</response>
        [HttpGet]
        public async Task<ActionResult<PageResult<BoardStatusDto>>> GetAll([FromQuery] FilterBoardStatusDto filter)
        {
            var response = await _mediator.Send(new GetAllBoardStatusQuery(filter));

            if (response is null)
            {
                return NotFound(new BaseResponse<PageResult<BoardStatusDto>>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no board status"
                });
            }

            return Ok(new BaseResponse<PageResult<BoardStatusDto>>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = response,
                Message = "Returning all records from BoardStatus table"
            });
        }

        /// <summary>
        /// Create BoardStatuse.
        /// Based on statusboardDto passed as parameter in request body.
        /// </summary>
        /// <response code="201">Issue correctly created.</response>
        /// <response code="400">Error creating BoardStatus.</response>
        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] BoardStatusDto dto)
        {
            var result = await _mediator.Send(new CreateBoardStatusCommand(dto));
            if (result == true)
            {
                return Ok(new BaseResponse<BoardStatusDto>
                {
                    ResponseCode = StatusCodes.Status201Created,
                    Data = dto,
                    Message = "BoardStatus created successfully"
                });
            }
            else
            {
                return BadRequest(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status400BadRequest,
                    Message = "Error creating BoardStatus"
                });
            }
        }

        /// <summary>
        /// Delete BoardStatus specifying boardId AND statusId.
        /// </summary>
        /// <response code="200">Resource deleted successfully.</response>
        /// <response code="400">An error occured trying to delete resource.</response>
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete([FromQuery] int boardId, [FromQuery] int statusId)
        {
            var result = await _mediator.Send(new DeleteBoardStatusCommand(boardId, statusId));
            if (result == true)
            {
                return Ok(new BaseResponse<IEnumerable<BoardStatusDto>>
                {
                    ResponseCode = StatusCodes.Status200OK,
                    Message = "Resource deleted successfully"
                });
            }
            else
            {
                return BadRequest(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status400BadRequest,
                    Message = "An error occured trying to delete resource"
                });
            }
        }
    }
}