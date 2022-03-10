using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.IssuesComments;

namespace Patronage.Api.Controllers
{
    [Route("api/issue/comments")]
    [ApiController]
    public class IssuesCommentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IssuesCommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{issueId}")]
        public ActionResult<PageResult<BaseCommentDto>> GetAllCommentFromIssue([FromRoute] int issueId)
        {
            
            return Ok();
        }

        [HttpPost("{issueId}")]
        public ActionResult Create([FromBody] BaseCommentDto dto)
        {

            return Ok();
        }

        [HttpPatch("{issueId}/{commentId}")]
        public ActionResult UpdateLight([FromBody] PartialCommentDto dto, [FromRoute] int issueId, [FromRoute] int commentId)
        {

            return Ok();
        }
    }
}
