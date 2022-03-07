using MediatR;

namespace Patronage.Api.MediatR.Issues.Commands.Assign
{
    public class AssignIssueCommand : IRequest<bool>
    {
        public int IssueId { get; set; }
        public string UserId { get; set; }

        public AssignIssueCommand(int id, string userId)
        {
            IssueId = id;
            UserId = userId;
        }
    }
}