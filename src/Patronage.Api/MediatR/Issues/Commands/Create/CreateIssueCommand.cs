using MediatR;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Commands
{
    public class CreateIssueCommand : IRequest<IssueDto>
    {
        public BaseIssueDto Data { get; set; } = null!;
    }
}