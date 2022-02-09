using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.Functions.Queries.GetIssues
{
    public class GetIssuesListQuery : IRequest<List<IssueDto>>
    {
        public string? SearchPhrase { get; set; }
        public DateTime Date { get; set; }
        public bool? IsActive { get; set;}
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
