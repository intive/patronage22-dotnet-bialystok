using MediatR;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Queries
{
    public class GetIssuesListQueryHandler : IRequestHandler<GetIssuesListQuery, PageResult<IssueDto>?>
    {
        private readonly IIssueService _issueService;

        public GetIssuesListQueryHandler(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public async Task<PageResult<IssueDto>?> Handle(GetIssuesListQuery request, CancellationToken cancellationToken)
        {
            return await _issueService.GetAllIssuesAsync(request.filter);
        }
    }
}
