using AutoMapper;
using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Issues.Commands.UpdateIssue
{
    public class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueCommand, Unit>
    {
        private readonly IIssueService _issueService;

        public UpdateIssueCommandHandler(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public Task<Unit> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
        {
            _issueService.Update(request.issueId, request.dto);

            return Task.FromResult(Unit.Value);
        }
    }
}
