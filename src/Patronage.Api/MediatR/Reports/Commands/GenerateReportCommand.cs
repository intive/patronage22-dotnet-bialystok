using MediatR;
using Patronage.Contracts.Helpers.Reports;

namespace Patronage.Api.MediatR.Reports.Commands
{
    public record GenerateReportCommand(GenerateReportParams Params) : IRequest;
}
