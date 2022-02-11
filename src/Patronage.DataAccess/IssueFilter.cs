﻿using Patronage.Contracts.ModelDtos;
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
        public static IQueryable<Issue> FilterBy(this IQueryable<Issue> value, GetAllIssuesQuery filter)
        {
            if (!string.IsNullOrEmpty(filter.SearchPhrase))
            {
                value = value.Where(x => x.Alias.Contains(filter.SearchPhrase) || x.Name.Contains(filter.SearchPhrase) || x.Description.Contains(filter.SearchPhrase));
            }

            return value;
        }
    }
}
