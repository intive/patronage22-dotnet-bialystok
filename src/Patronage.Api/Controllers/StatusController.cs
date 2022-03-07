using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.Status.Commands;
using Patronage.Api.MediatR.Status.Queries;
using Patronage.Contracts.ModelDtos;
using Patronage.DataAccess;
using Swashbuckle.AspNetCore.Annotations;
namespace Patronage.Api.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatusController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [SwaggerOperation(Summary = "Get all Statuses", Description = "Returns all Statuses records available in the database")]
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Returning all Statuses")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No Statuses were found in the database")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Sorry, try it later")]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetAll()
        {
            var response = await _mediator.Send(new GetAllStatusQuery());
            if (response is not null)
            {
                return Ok(new BaseResponse<IEnumerable<StatusDto>>
                {
                    ResponseCode = StatusCodes.Status200OK,
                    Data = response,
                    Message = "Returning all Statuses"
                });
            }
            return NotFound(new BaseResponse<IEnumerable<StatusDto>>());
        }
        [SwaggerOperation(Summary = "Get status with id")]
        [HttpGet("id")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returning all records matching provided criteria")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No records with provided statusId were found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Sorry, try it later")]
        public async Task<ActionResult<StatusDto>> GetById(int id)
        {
            var response = await _mediator.Send(new GetByIdStatusQuerry(id));
            if (response is not null)
            {
                return Ok(new BaseResponse<StatusDto>
                {
                    ResponseCode = StatusCodes.Status200OK,
                    Data = response,
                    Message = $"Returning status with id {response.Id}"
                });
            }
            return NotFound(new BaseResponse<StatusDto>
            {
                ResponseCode = StatusCodes.Status404NotFound,
                Message = "Status not found"

            });

        }

        [SwaggerOperation(Summary = "Create new Status", Description = "Create Status with string code")]
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "Status created successfully")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Validation error")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Sorry, try it later")]
        public async Task<ActionResult<int>> Create([FromQuery] string code)
        {
            var id = await _mediator.Send(new CreateStatusCommand(code));

                return Created($"/api/status/{id}", new BaseResponse<int>
                {
                    ResponseCode = StatusCodes.Status201Created,
                    Data = id,
                    Message = "Status created successfully"
                });
      
        }

        [SwaggerOperation(Summary = "Update Status", Description = "Update status code providing statusId and updated status code")]
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, "Resource updated successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error occured trying to update resource")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Validation error")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Sorry, try it later")]
        public async Task<ActionResult<bool>> Update([FromQuery] int id, [FromQuery] string code)
        {
            var isSucceded = await _mediator.Send(new UpdateStatusCommand(id, code));
            if (isSucceded)
            {
            return Ok(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status201Created,
                Data = isSucceded,
                Message = "Status updated successfully"
            });
            }
            return BadRequest(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status400BadRequest,
                Data = isSucceded,
                Message = "An error occured trying to update resource"
            });

        }

        [SwaggerOperation(Summary = "Delete Status by id", Description = "Delete Status specifying statusId")]
        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status200OK, "Resource deleted successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error occured trying to delete resource")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Sorry, try it later")]
        public async Task<ActionResult<bool>> Delete([FromQuery] int id)
        {
            var isSucceded = await _mediator.Send(new DeleteStatusCommand(id));
            if (isSucceded)
            {
                return Ok(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status200OK,
                    Message = "Resource deleted successfully"
                });
            }
            return BadRequest(new BaseResponse<bool>
            {
                ResponseCode = StatusCodes.Status400BadRequest,
                Message = "An error occured trying to delete resource"
            });
        }
    }
}
