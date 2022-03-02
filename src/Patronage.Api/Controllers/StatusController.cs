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
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Error creating new Status")]
        public async Task<ActionResult<int>> Create([FromQuery] string code)
        {
            int? id = await _mediator.Send(new CreateStatusCommand(code));
            if (id is not null)
            {
                return Created($"/api/status/{id}", new BaseResponse<int>
                {
                    ResponseCode = StatusCodes.Status201Created,
                    Data = (int)id,
                    Message = "Status created successfully"
                });
            }            
            return BadRequest(new BaseResponse<string>
            {
                ResponseCode = StatusCodes.Status400BadRequest,
                Data = null,
                Message = "Status code already exists"
            });
        }
        [HttpPut]
        public async Task<ActionResult<bool>> Update([FromQuery] int id, [FromQuery] string code)
        {
            var status = await _mediator.Send(new UpdateStatusCommand(id, code));
            return status;
        }
        [SwaggerOperation(Summary = "Delete Status by id", Description = "Delete Status specifying statusId")]
        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status200OK, "Resource deleted successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error occured trying to delete resource")]
        public async Task<ActionResult<bool>> Delete([FromQuery] int id)
        {
            var deleted = await _mediator.Send(new DeleteStatusCommand(id));
            if (deleted)
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
