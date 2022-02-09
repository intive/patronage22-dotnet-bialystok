using AutoMapper;
using MediatR;
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
    public class GetAllIssuesQueryHandler : IRequestHandler<GetAllIssuesQuery, List<IssueDto>>
    {
        private readonly IIssueService _issueService;
        private readonly IMapper _mapper;

        public GetAllIssuesQueryHandler(IIssueService issueService, IMapper mapper)
        {
            _issueService = issueService;
            _mapper = mapper;
        }

        public Task<List<IssueDto>> Handle(GetAllIssuesQuery request, CancellationToken cancellationToken)
        {
            var issues = _issueService.GetAllIssues().AsQueryable();

            issues = issues.FilterBy(request);
            issues = issues
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize);

            var issueDto = _mapper.Map<List<IssueDto>>(issues);

            return Task.FromResult(issueDto);
        }
    }
}
