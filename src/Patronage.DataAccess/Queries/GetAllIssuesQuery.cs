using MediatR;
using Patronage.Common.Enums;
using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.DataAccess.Queries
{
    public class GetAllIssuesQuery : IRequest<List<Issue>>
    {
        public string SearchPhrase { get; set; }
        public DateTime Date { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
