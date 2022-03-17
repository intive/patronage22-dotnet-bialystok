using Patronage.Contracts.ModelDtos.Issues;
using Patronage.Models;

namespace Patronage.Contracts.Helpers
{
    public static class IssueQueryExtensions
    {
        public static IQueryable<Issue> FilterBy(this IQueryable<Issue> value, FilterIssueDto filter)
        {
            if (!string.IsNullOrEmpty(filter.SearchPhrase))
            {
                value = value.Where(x => x.Alias.Contains(filter.SearchPhrase) || x.Name.Contains(filter.SearchPhrase) || x.Description!.Contains(filter.SearchPhrase));
            }
            // here will be other filters in the future

            return value;
        }
    }
}