using MediatR;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Commands.UpdateIssue
{
    public record UpdateIssueCommand(int issueId, BaseIssueDto dto) : IRequest;
}
