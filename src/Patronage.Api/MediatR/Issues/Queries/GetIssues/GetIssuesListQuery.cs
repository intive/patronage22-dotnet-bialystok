using MediatR;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Issues.Queries.GetIssues
{
    public class GetIssuesListQuery : IRequest<PageResult<IssueDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
