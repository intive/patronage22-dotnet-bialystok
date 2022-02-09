using MediatR;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.DataAccess.Queries
{
    public class GetAllIssuesQuery : IRequest<List<IssueDto>>
    {
        public string? SearchPhrase { get; set; }
        public DateTime Date { get; set; }
        public bool? IsActive { get; set;}
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
