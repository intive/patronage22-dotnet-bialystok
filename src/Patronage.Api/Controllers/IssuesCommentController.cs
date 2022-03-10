using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.IssuesComments;

namespace Patronage.Api.Controllers
{
    [Route("api/issue/comments")]
    [ApiController]
    public class IssuesCommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IssuesCommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{issueId}")]
        public ActionResult<PageResult<BaseIssueCommentDto>> GetAllCommentFromIssue([FromRoute] int issueId)
        {
            
            return Ok();
        }

        [HttpPost("{issueId}")]
        public ActionResult Create([FromRoute] int issueId)
        {

            return Ok();
        }

        [HttpPatch("{issueId}/{commentId}")]
        public ActionResult UpdateLight([FromBody] PartialIssueCommentDto dto, [FromRoute] int issueId, [FromRoute] int commentId)
        {

            return Ok();
        }

        [HttpDelete("{issueId}/{commentId}")]
        public ActionResult Delete([FromRoute] int issueId, [FromRoute] int commentId)
        {

            return Ok();
        }
    }
}
