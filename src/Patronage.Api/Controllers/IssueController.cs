using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.Issues.Commands.CreateIssue;
using Patronage.Api.MediatR.Issues.Commands.DeleteIssue;
using Patronage.Api.MediatR.Issues.Commands.LightUpdateIssue;
using Patronage.Api.MediatR.Issues.Commands.UpdateIssue;
using Patronage.Api.MediatR.Issues.Queries.GetIssues;
using Patronage.Api.MediatR.Issues.Queries.GetSingleIssue;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.Issues;
using Patronage.DataAccess;
using Swashbuckle.AspNetCore.Annotations;

namespace Patronage.Api.Controllers
{
    [Route("api/issue")]
    [ApiController]
    [Authorize]
    public class IssueController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IssueController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [SwaggerOperation(Summary = "Returns all Issues")]
        [HttpGet("list")]
        public async Task<ActionResult<PageResult<IssueDto>>> GetAllIssues([FromQuery] FilterIssueDto filter)
        {
            var result = await _mediator.Send(new GetIssuesListQuery(filter));
            if (result is null)
            {
                return NotFound(new BaseResponse<PageResult<IssueDto>>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no issues"
                });
            }

            return Ok(new BaseResponse<PageResult<IssueDto>>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }

        [SwaggerOperation(Summary = "Returns Issue by id")]
        [HttpGet("{issueId}")]
        public async Task<ActionResult<IssueDto>> GetIssueById([FromRoute] int issueId)
        {
            var result = await _mediator.Send(new GetSingleIssueQuery(issueId));
            if (result is null)
            {
                return NotFound(new BaseResponse<IssueDto>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no issue with Id: {issueId}"
                });
            }

            return Ok(new BaseResponse<IssueDto>
            {
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }

        [SwaggerOperation(Summary = "Creates Issue")]
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateIssueCommand command)
        {
            var result = await _mediator.Send(command);
            if (result is null)
            {
                return BadRequest(new BaseResponse<IssueDto>
                {
                    ResponseCode = StatusCodes.Status400BadRequest,
                });
            }

            return Ok(new BaseResponse<IssueDto>
            {
                Message = "Issue was created successfully",
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }

        [SwaggerOperation(Summary = "Updates Issue")]
        [HttpPut("update/{issueId}")]
        public async Task<ActionResult> Update([FromBody] BaseIssueDto dto, [FromRoute] int issueId)
        {
            var result = await _mediator.Send(new UpdateIssueCommand(issueId, dto));
            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no issue with Id: {issueId}"
                });
            }

            return Ok(new BaseResponse<bool>
            {
                Message = "Issue was updated successfully",
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }

        [SwaggerOperation(Summary = "Light Update Issue")]
        [HttpPatch("updateLight/{issueId}")]
        public async Task<ActionResult> UpdateLight([FromBody] PartialIssueDto dto, [FromRoute] int issueId)
        {
            var result = await _mediator.Send(new UpdateLightIssueCommand(issueId, dto));
            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no issue with Id: {issueId}"
                });
            }

            return Ok(new BaseResponse<bool>
            {
                Message = "Issue was updated successfully",
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }

        [SwaggerOperation(Summary = "Soft delete Issue by Id")]
        [HttpDelete("delete/{issueId}")]
        public async Task<ActionResult> Delete([FromRoute] int issueId)
        {
            var result = await _mediator.Send(new DeleteIssueCommand(issueId));
            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = $"There's no issue with Id: {issueId}"
                });
            }

            return Ok(new BaseResponse<bool>
            {
                Message = "Issue was deleted successfully",
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }
    }
}
