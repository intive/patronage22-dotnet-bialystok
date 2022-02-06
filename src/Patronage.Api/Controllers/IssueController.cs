using Microsoft.AspNetCore.Mvc;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Swashbuckle.AspNetCore.Annotations;

namespace Patronage.Api.Controllers
{
    [Route("api/issue")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;

        public IssueController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [SwaggerOperation(Summary = "Returns all Issues")]
        [HttpGet("list")]
        public ActionResult<IEnumerable<IssueDto>> GetAll()
        {
            var issues = _issueService.GetAll();

            return Ok(issues);
        }

        [SwaggerOperation(Summary = "Returns Issue by id")]
        [HttpGet("{issueId}")]
        public ActionResult<IssueDto> GetById([FromRoute] int issueId)
        {
            var issue = _issueService.GetById(issueId);

            return Ok(issue);
        }

        [SwaggerOperation(Summary = "Creates Issue")]
        [HttpPost("create")]
        public ActionResult Create([FromBody] CreateIssueDto dto)
        {
            var id = _issueService.Create(dto);

            return Created($"/api/issue/{id}", null);
        }

        [SwaggerOperation(Summary = "Updates Issue")]
        [HttpPost("update/{issueId}")]
        public ActionResult Update([FromBody] UpdateIssueDto dto, [FromRoute] int issueId)
        {
            _issueService.Update(issueId, dto);

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
