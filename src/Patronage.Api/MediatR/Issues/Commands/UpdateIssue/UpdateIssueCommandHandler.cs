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
            var issue = _issueService.GetById(request.Id);

            issue.Alias = request.Dto.Alias;
            issue.Name = request.Dto.Name;
            issue.Description = request.Dto.Description;
            issue.ProjectId = request.Dto.ProjectId;
            issue.BoardId = request.Dto.BoardId;
            issue.StatusId = request.Dto.StatusId;

            _issueService.Update(issue);

            return Task.FromResult(Unit.Value);
        }
    }
}
