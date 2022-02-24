using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.BoardStatus.Commands;
using Patronage.Api.MediatR.BoardStatus.Queries;
using Patronage.Api.MediatR.Status.Queries;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.DataAccess;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
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
        [SwaggerResponse(StatusCodes.Status204NoContent, "No Statuses were found in the database")]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetAll()
        {
            var response = await _mediator.Send(new GetAllStatusQuery());

            if (response.Any())
            { // TODO: Ask for return code || Ok || No content
                return NoContent();
            }

            return Ok(new BaseResponse<IEnumerable<StatusDto>>
            {

                ResponseCode = StatusCodes.Status200OK,
                Data = response,
                Message = "Returning all records from BoardStatus table"
            });
        }
    }
}
