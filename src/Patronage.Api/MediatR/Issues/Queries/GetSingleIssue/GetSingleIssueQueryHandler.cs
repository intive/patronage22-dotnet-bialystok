using MediatR;
using Patronage.Api.Exceptions;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Queries.GetSingleIssue
{
    public class GetSingleIssueQueryHandler : IRequestHandler<GetSingleIssueQuery, IssueDto?>
    {
        private readonly IIssueService _issueService;

        public GetSingleIssueQueryHandler(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public async Task<IssueDto?> Handle(GetSingleIssueQuery request, CancellationToken cancellationToken)
        {
            var result = await _issueService.GetByIdAsync(request.id);

            if (result is null)
            {
                return null;
            }

            var issueDto = new IssueDto()
            {
                Id = result.Id,
                Alias = result.Alias,
                Name = result.Name,
                Description = result.Description,
                ProjectId = result.ProjectId,
                BoardId = result.BoardId,
                CreatedOn = result.CreatedOn,
                ModifiedOn = result.ModifiedOn,
                StatusId = result.StatusId,
                IsActive = result.IsActive
            };

            return issueDto;
        }
    }
}
