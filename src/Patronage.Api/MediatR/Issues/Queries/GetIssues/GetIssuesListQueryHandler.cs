using MediatR;
using Patronage.Api.MediatR.Extensions;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Queries.GetIssues
{
    public class GetIssuesListQueryHandler : IRequestHandler<GetIssuesListQuery, PageResult<IssueDto>>
    {
        private readonly IIssueService _issueService;

        public GetIssuesListQueryHandler(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public Task<PageResult<IssueDto>> Handle(GetIssuesListQuery request, CancellationToken cancellationToken)
        {
            var baseQuery = _issueService.GetAllIssues();

            baseQuery = baseQuery.FilterBy(request);
            var totalItemCount = baseQuery.Count();

            var issues = baseQuery
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize);

            List<IssueDto> issuesDto = new List<IssueDto>();
            foreach (var issue in issues)
            {
                issuesDto.Add(new IssueDto
                {
                    Id = issue.Id,
                    CreatedOn = issue.CreatedOn,
                    ModifiedOn = issue.ModifiedOn,
                    IsActive = issue.IsActive,
                    Alias = issue.Alias,
                    Name = issue.Name,
                    Description = issue.Description,
                    ProjectId = issue.ProjectId,
                    BoardId = issue.BoardId,
                    StatusId = issue.StatusId
                });
            }

            var result = new PageResult<IssueDto>(issuesDto, totalItemCount, request.PageSize, request.PageNumber);
            return Task.FromResult(result);
        }
    }
}
