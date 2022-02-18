using MediatR;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Commands.UpdateIssue
{
    public class UpdateIssueCommand : IRequest
    {
        public int Id { get; set; }
        public BaseIssueDto Dto { get; set; }
    }
}
