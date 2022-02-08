using Patronage.Contracts.ModelDtos;
using Patronage.DataAccess.Queries;
using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.DataAccess
{
    public static class IssueFilter
    {
        public static List<Issue> FilterBy(this List<Issue> value, GetAllIssuesQuery filter)
        {
            if (!string.IsNullOrEmpty(filter.SearchPhrase))
            {
                value = value.Where(x => x.Alias.Contains(filter.SearchPhrase) || x.Name.Contains(filter.SearchPhrase) || x.Description.Contains(filter.SearchPhrase)).ToList();
            }
            if (filter.Date > DateTime.MinValue)
            {
                value = value.Where(x => x.CreatedOn.Day == filter.Date.Day && x.CreatedOn.Month == filter.Date.Month && x.CreatedOn.Year == filter.Date.Year ||
                                         x.ModifiedOn.Day == filter.Date.Day && x.ModifiedOn.Month == filter.Date.Month && x.ModifiedOn.Year == filter.Date.Year).ToList();
            }

            return value;
        }
    }
}
