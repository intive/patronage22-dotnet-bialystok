using MediatR;
using Patronage.Api.Exceptions;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Queries.GetSingleIssue
{
    public class GetSingleIssueQueryHandler : IRequestHandler<GetSingleIssueQuery, IssueDto>
    {
        private readonly IIssueService _issueService;

        public GetSingleIssueQueryHandler(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public Task<IssueDto> Handle(GetSingleIssueQuery request, CancellationToken cancellationToken)
        {
            var issue = _issueService.GetById(request.id);

            if (issue == null)
            {
                throw new NotFoundException("Issues not found");
            }

            var issueDto = new IssueDto
            {
                Id = issue.Id,
                Alias = issue.Alias,
                Name = issue.Name,
                Description = issue.Description,
                ProjectId = issue.ProjectId,
                BoardId = issue.BoardId,
                StatusId = issue.StatusId,
                IsActive = issue.IsActive,
                CreatedOn = issue.CreatedOn,
                ModifiedOn = issue.ModifiedOn
            };

            return Task.FromResult(issueDto);
        }
    }
}
