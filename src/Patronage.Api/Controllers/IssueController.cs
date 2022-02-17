using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.Issues.Commands.CreateIssue;
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
        public async Task<ActionResult> Create([FromBody] BaseIssueDto dto)
        {
            var id = await _mediator.Send(new CreateIssueCommand(dto));

            return Created($"/api/issue/{id}", null);
        }

        [SwaggerOperation(Summary = "Updates Issue")]
        [HttpPost("update/{issueId}")]
        public async Task<ActionResult> Update([FromBody] BaseIssueDto dto, [FromRoute] int issueId)
        {
            await _mediator.Send(new UpdateIssueCommand(issueId, dto));

            return Ok();
        }

        [SwaggerOperation(Summary = "Light Updates Issue")]
        [HttpPost("updateLight/{issueId}")]
        public ActionResult UpdateLight([FromBody] BaseIssueDto dto, [FromRoute] int issueId)
        {
            _issueService.LightUpdate(issueId, dto);

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
