using MediatR;
using Patronage.Api.Exceptions;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Issues.Commands.DeleteIssue
{
    public class DeleteIssueCommandHandler : IRequestHandler<DeleteIssueCommand, Unit>
    {
        private readonly IIssueService _issueService;

        public DeleteIssueCommandHandler(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public Task<Unit> Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
        {
            var issue = _issueService.GetById(request.Id);

            if (issue == null)
            {
                throw new NotFoundException("Issue not found");
            }

            issue.IsActive = false;

            _issueService.Save();

            return Task.FromResult(Unit.Value);
        }
    }
}
