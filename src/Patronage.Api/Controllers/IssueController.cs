using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.Issues.Commands.CreateIssue;
using Patronage.Api.MediatR.Issues.Commands.LightUpdateIssue;
using Patronage.Api.MediatR.Issues.Commands.UpdateIssue;
using Patronage.Api.MediatR.Issues.Queries.GetIssues;
using Patronage.Api.MediatR.Issues.Queries.GetSingleIssue;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Issues;
using Swashbuckle.AspNetCore.Annotations;

namespace Patronage.Api.Controllers
{
    [Route("api/issue")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;
        private readonly IMediator _mediator;

        public IssueController(IIssueService issueService, IMediator mediator)
        {
            _issueService = issueService;
            _mediator = mediator;
        }

        [SwaggerOperation(Summary = "Returns all Issues")]
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<IssueDto>>> GetAllIssues([FromQuery] GetIssuesListQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [SwaggerOperation(Summary = "Returns Issue by id")]
        [HttpGet("{issueId}")]
        public async Task<ActionResult<IssueDto>> GetIssueById([FromRoute] int issueId)
        {
            var result = await _mediator.Send(new GetSingleIssueQuery(issueId));

            return Ok(result);
        }

        [SwaggerOperation(Summary = "Creates Issue")]
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateIssueCommand command)
        {
            var id = await _mediator.Send(command);

            return Created($"/api/issue/{id}", null);
        }

        [SwaggerOperation(Summary = "Updates Issue")]
        [HttpPut("update/{issueId}")]
        public async Task<ActionResult> Update([FromBody] BaseIssueDto dto, [FromRoute] int issueId)
        {
            var command = new UpdateIssueCommand()
            {
                Id = issueId,
                Dto = dto
            };
            await _mediator.Send(command);

            return Ok();
        }

        [SwaggerOperation(Summary = "Light Updates Issue")]
        [HttpPut("updateLight/{issueId}")]
        public async Task<ActionResult> UpdateLight([FromBody] PartialIssueDto dto, [FromRoute] int issueId)
        {
            var command = new UpdateLightIssueCommand()
            {
                Id = issueId,
                Dto = dto
            };
            await _mediator.Send(command);

            return Ok();
        }

        [SwaggerOperation(Summary = "Deletes Issue")]
        [HttpDelete("delete/{issueId}")]
        public ActionResult Delete([FromRoute] int issueId)
        {
            _issueService.Delete(issueId);

            return Ok();
        }
    }
}
