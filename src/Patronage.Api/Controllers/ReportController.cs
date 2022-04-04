using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patronage.Api.MediatR.Reports.Commands;
using Patronage.Api.MediatR.Reports.Queries;
using Patronage.Contracts.Helpers.Reports;
using Patronage.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Commition to generate a report of specified type. In the response there is a ID of report.
        /// You can check a status of generation and download a report using this ID.
        /// </summary>
        /// <param name="reportType">The type of report to generate. By default it is TaskCountReport.
        /// Instead of this you can choose: ... (Now there is only one type of report availabe)</param>
        /// <response code="202">Repert accepted to preparation</response>
        [HttpPost("generate")]
        public async Task<ActionResult<string>> GenerateReport([FromQuery]ReportType reportType = ReportType.TaskCountReport)
        {
            var reportParams = new GenerateReportParams
            {
                Type = reportType,
                ReportId = Guid.NewGuid().ToString(),
                UserId = HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value
            };

            await _mediator.Send(new GenerateReportCommand(reportParams));

            return Accepted(new BaseResponse<string>
            {
                ResponseCode = StatusCodes.Status202Accepted,
                Message = "There is a guid which whom you can chceck a creating report status and download it when status is Generated",
                Data = reportParams.ReportId
            });
        }

        /// <summary>
        /// Checking if a report is done and ready to download or if is stil in progress.
        /// </summary>
        /// <param name="reportGuid">Here you have to give a ID of report which status you want to get to know.</param>
        /// <response code="200">Report status</response>
        [HttpGet("checkStatus")]
        public async Task<ActionResult> GetReportStatus(string reportGuid)
        {
            var reportStatus = await _mediator.Send(new GetReprtStatusQuery(reportGuid));

            return Ok(new BaseResponse<string>
            {
                ResponseCode = StatusCodes.Status200OK,
                Message = $"The status of report generation is: {reportStatus}"
            });
        }

        /// <summary>
        /// Downloading a prepared report.
        /// </summary>
        /// <param name="reportGuid">Here you have to give a ID of report you want to get.</param>
        /// <response code="200">Report downloaded</response>
        /// <response code="404">There is no report with this ID</response>
        [HttpPost("download")]
        public async Task<ActionResult> DownloadReport(string reportGuid)
        {
            await _mediator.Send(new DownloadReportCommand(reportGuid));

            return Ok();
        }
    }
}
