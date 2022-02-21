using MediatR;

namespace Patronage.Api.MediatR.Issues.Commands.DeleteIssue
{
    public class DeleteIssueCommand : IRequest
    {
        public int Id { get; set; }
    }
}
