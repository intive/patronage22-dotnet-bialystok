using Microsoft.AspNetCore.Mvc;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;

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

        [HttpPost("create")]
        public ActionResult Create([FromBody] CreateIssueDto dto)
        {

            return Ok();
        }

        [HttpDelete("delete/{issueId}")]
        public ActionResult Delete(int issueId)
        {

            return Ok();
        }

        [HttpPost("update/{issueId}")]
        public ActionResult Update(int issueId, UpdateIssueDto dto)
        {

            return Ok();
        }
    }
}
