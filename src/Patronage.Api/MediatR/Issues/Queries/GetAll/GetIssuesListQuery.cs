using MediatR;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Queries
{
    public class GetIssuesListQuery : IRequest<PageResult<IssueDto>>
    {
        public FilterIssueDto filter;

        public GetIssuesListQuery(FilterIssueDto filter)
        {
            this.filter = filter;
        }
    }
}