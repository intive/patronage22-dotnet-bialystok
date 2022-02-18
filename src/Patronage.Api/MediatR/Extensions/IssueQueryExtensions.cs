using Patronage.Api.MediatR.Issues.Queries.GetIssues;
using Patronage.Models;

namespace Patronage.Api.MediatR.Extensions
{
    public static class IssueQueryExtensions
    {
        public static IQueryable<Issue> FilterBy(this IQueryable<Issue> value, GetIssuesListQuery filter)
        {
            if (!string.IsNullOrEmpty(filter.SearchPhrase))
            {
                value = value.Where(x => x.Alias.Contains(filter.SearchPhrase) || x.Name.Contains(filter.SearchPhrase) || x.Description.Contains(filter.SearchPhrase));
            }

            return value;
        }
    }
}
