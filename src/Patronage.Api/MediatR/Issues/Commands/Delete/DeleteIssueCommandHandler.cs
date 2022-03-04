using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Issues.Commands.Delete
{
    public class DeleteIssueCommandHandler : IRequestHandler<DeleteIssueCommand, bool>
    {
        private readonly IIssueService _issueService;

        public DeleteIssueCommandHandler(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public async Task<bool> Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
        {
            return await _issueService.DeleteAsync(request.Id);
        }
    }
}
