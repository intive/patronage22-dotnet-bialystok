using MediatR;

namespace Patronage.Api.MediatR.Reports.Commands
{
    public record DownloadReportCommand(string reportId) : IRequest;
}
