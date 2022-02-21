using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.BoardStatus.Commands;
using Patronage.Api.MediatR.BoardStatus.Queries;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.DataAccess;
using Swashbuckle.AspNetCore.Annotations;

namespace Patronage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardStatusController : ControllerBase
    {
        private readonly IBoardStatusService _boardStatusService;
        private readonly IMediator _mediator;

        public BoardStatusController(IBoardStatusService boardStatusService, IMediator mediator )
        {
            _boardStatusService = boardStatusService;
            _mediator = mediator;
        }

        [SwaggerOperation(Summary = "Get all BoardStatus", Description ="Returns all BoardStatus records available in the database")]
        [HttpGet]        
        [SwaggerResponse(StatusCodes.Status200OK, "Returning all records from BoardStatus table")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "No BoardStatus found in the database")]
        public async Task<ActionResult<IEnumerable<BoardStatusDto>>> GetAll()
        {
            var response = await _mediator.Send(new GetAllBoardStatusQuery());

            if (response.Any() == false)
            { // TODO: Ask for return code || Ok || No content
                return NoContent();
                //    return Ok(new BaseResponse<bool>
                //{
                //    ResponseCode = StatusCodes.Status204NoContent
                //});
            }

            return Ok(new BaseResponse<IEnumerable<BoardStatusDto>>
            {

                ResponseCode = StatusCodes.Status200OK,
                Data =response,
                Message = "Returning all records from BoardStatus table"
            });
        }

        [SwaggerOperation(Summary = "Get StatusBoard by boardId, statusId", Description ="Find all BoardStatus with specified boardId OR statusId OR both")]
        [HttpGet("id")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returning all records matching provided criteria")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "No records with provided boardId or statusId were found")]
        public async Task<ActionResult<IEnumerable<BoardStatusDto>>> GetById([FromQuery] int boardId,[FromQuery] int statusId)
        {
            var response = await _mediator.Send(new GetByIdBoardStatusQuery(boardId, statusId));
            if (response.Any() == false)
            {
                return NoContent();
                //TODO: Not found or no content? 
                //    (new BaseResponse<bool>
                //{
                //    ResponseCode = StatusCodes.Status404NotFound
                //});
            }

            return Ok(new BaseResponse<IEnumerable<BoardStatusDto>>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = response,
                Message = "Returning all records matching provided criteria"
            });
        }

        [SwaggerOperation(Summary = "Create BoardStatus", Description = "Create BoardStatus based on statusboardDto passed as parameter in request body")]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "BoardStatus created successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error creating BoardStatus")]
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

        [SwaggerOperation(Summary = "Delete BoardStatus by id", Description ="Delete BoardStatus specifying boardId AND statusId")]
        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status200OK, "Resource deleted successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error occured trying to delete resource")]
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
