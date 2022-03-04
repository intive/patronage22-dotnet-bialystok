using MediatR;

namespace Patronage.Api.MediatR.Issues.Commands.Assign
{
    public class AssignIssueCommand : IRequest<bool>
    {
        public int IssueId { get; set; }
        public int UserId { get; set; }

        public AssignIssueCommand(int id, int userId)
        {
            IssueId = id;
            UserId = userId;
        }
    }
}