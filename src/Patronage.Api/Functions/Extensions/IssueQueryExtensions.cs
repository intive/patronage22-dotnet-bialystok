using Patronage.Api.Functions.Queries.GetIssues;
using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.Functions.Extensions
{
    public static class IssueQueryExtensions
    {
        public static IQueryable<Issue> FilterBy(this IQueryable<Issue> value, GetIssuesListQuery filter)
        {
            if (!string.IsNullOrEmpty(filter.SearchPhrase))
            {
                value = value.Where(x => x.Alias.Contains(filter.SearchPhrase) || x.Name.Contains(filter.SearchPhrase) || x.Description.Contains(filter.SearchPhrase));
            }
            if (filter.Date > DateTime.MinValue)
            {

            }
            if (filter.IsActive.HasValue)
            {
                value = value.Where(x => x.IsActive == filter.IsActive);
            }

            return value;
        }
    }
}
