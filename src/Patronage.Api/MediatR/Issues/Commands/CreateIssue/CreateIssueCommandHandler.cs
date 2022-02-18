using MediatR;
using Patronage.Api.MediatR.Issues.Commands.CreateIssue;
using Patronage.Contracts.Interfaces;
using Patronage.Models;

namespace Patronage.Api.MediatR.Projects.Commands.CreateProject
{
    public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, int>
    {
        private readonly IIssueService _issueService;

        public CreateIssueCommandHandler(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public Task<int> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            var issue = new Issue
            {
                Alias = request.Alias,
                Name = request.Name,
                Description = request.Description,
                ProjectId = request.ProjectId,
                BoardId = request.BoardId,
                StatusId = request.StatusId,
                IsActive = true
            };

            var id = _issueService.Create(issue);

            return Task.FromResult(id);
        }
    }
}
