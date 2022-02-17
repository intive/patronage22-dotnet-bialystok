using MediatR;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Commands.CreateIssue
{
    public record CreateIssueCommand(BaseIssueDto dto) : IRequest;
}
