using AutoMapper;
using MediatR;
using Patronage.Common.Enums;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.DataAccess.Queries;
using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.DataAccess.QueryHandlers
{
    public class GetAllIssuesQueryHandler : IRequestHandler<GetAllIssuesQuery, List<Issue>>
    {
        private readonly TableContext _dbContext;
        private readonly IIssueService _issueService;
        //private readonly IMapper _mapper;

        public GetAllIssuesQueryHandler(TableContext dbContext, IIssueService issueService)//, IMapper mapper)
        {
            _dbContext = dbContext;
            _issueService = issueService;
            //_mapper = mapper;
        }


        Task<List<Issue>> IRequestHandler<GetAllIssuesQuery, List<Issue>>.Handle(GetAllIssuesQuery request, CancellationToken cancellationToken)
        {
            var issues = _issueService.GetAll();

            issues = issues.FilterBy(request);

            //var issueDto = _mapper.Map<List<IssueDto>>(issues);

            var fullOfQueryIssues = issues
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize)
                .ToList();

            return Task.FromResult(fullOfQueryIssues);
        }
    }
}
