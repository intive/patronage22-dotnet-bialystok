using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Issues.Commands
{
    public class AssignIssueCommandHandler : IRequestHandler<AssignIssueCommand, bool>
    {
        private readonly IIssueService _issueService;

        public AssignIssueCommandHandler(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public async Task<bool> Handle(AssignIssueCommand request, CancellationToken cancellationToken)
        {
            return await _issueService.AssignUserAsync(request.IssueId, request.UserId);
        }
    }
}
