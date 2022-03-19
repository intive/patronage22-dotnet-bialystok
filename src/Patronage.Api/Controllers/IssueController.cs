using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.Issues.Commands;
using Patronage.Api.MediatR.Issues.Queries;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.Issues;
using Patronage.DataAccess;

namespace Patronage.Api.Controllers
{
    [Route("api/issue")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IssueController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all Issues. When you give "SearchPhrase" in Query you will receive only issue
        /// in which name, alias or description contains this phrase.
        /// You need to add the PageSize and PageNumber.
        /// </summary>
        /// <response code="200">Searched issues.</response>
        /// <response code="404">Issues not found.</response>
        [HttpGet]
        public async Task<ActionResult<BaseResponse<PageResult<IssueDto>>>> GetAllIssues([FromQuery] FilterIssueDto filter)
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

        /// <summary>
        /// Returns Issue by id.
        /// </summary>
        /// <response code="200">Searched issue.</response>
        /// <response code="404">Issue not found.</response>
        [HttpGet("{issueId}")]
        public async Task<ActionResult<BaseResponse<IssueDto>>> GetIssueById([FromRoute] int issueId)
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

        /// <summary>
        /// Creates Issue.
        /// </summary>
        /// <response code="201">Issue correctly created.</response>
        /// <response code="400">Please insert correct JSON object with parameters.</response>
        [HttpPost]
        public async Task<ActionResult<BaseResponse<bool>>> Create([FromBody] CreateIssueCommand command)
        {
            var result = await _mediator.Send(command);
            if (result is null)
            {
                return BadRequest(new BaseResponse<IssueDto>
                {
                    ResponseCode = StatusCodes.Status400BadRequest,
                });
            }

            return CreatedAtAction(nameof(Create), new BaseResponse<IssueDto>
            {
                Message = "Issue was created successfully",
                ResponseCode = StatusCodes.Status201Created,
                Data = result
            });
        }

        /// <summary>
        /// Updates issue - it's all properties.
        /// </summary>
        /// <response code="200">Issue correctly updated.</response>
        /// <response code="400">Please insert correct JSON object with parameters.</response>
        /// <response code="404">Issue not found.</response>
        [HttpPut("{issueId}")]
        public async Task<ActionResult<BaseResponse<bool>>> Update([FromBody] BaseIssueDto dto, [FromRoute] int issueId)
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

        /// <summary>
        /// Updates issue - only selected properties.
        /// </summary>
        /// <response code="200">Issue correctly updated.</response>
        /// <response code="400">Please insert correct JSON object with parameters.</response>
        /// <response code="404">Issue not found.</response>
        [HttpPatch("{issueId}")]
        public async Task<ActionResult<BaseResponse<bool>>> UpdateLight([FromBody] PartialIssueDto dto, [FromRoute] int issueId)
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

        /// <summary>
        /// Deletes Issue. (Changes flag "IsActive" to false - Soft delete))
        /// </summary>
        /// <response code="200">Issue correctly deleted.</response>
        /// <response code="404">Issue not found.</response>
        [HttpDelete("{issueId}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete([FromRoute] int issueId)
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

        /// <summary>
        /// Assigns issue to user.
        /// </summary>
        /// <response code="200">User has assigned correctly.</response>
        /// <response code="404">Issue or user not found.</response>
        [HttpPut("{issueId}/assign/{userId}")]
        public async Task<ActionResult<BaseResponse<bool>>> Assign([FromRoute] int issueId, [FromRoute] string userId)
        {
            var result = await _mediator.Send(new AssignIssueCommand(issueId, userId));
            if (!result)
            {
                return NotFound(new BaseResponse<bool>
                {
                    ResponseCode = StatusCodes.Status404NotFound,
                    Message = "Issue or user with given Id not found"
                });
            }

            return Ok(new BaseResponse<bool>
            {
                Message = "The user has been assigned to the issue successfully",
                ResponseCode = StatusCodes.Status200OK,
                Data = result
            });
        }
    }
}