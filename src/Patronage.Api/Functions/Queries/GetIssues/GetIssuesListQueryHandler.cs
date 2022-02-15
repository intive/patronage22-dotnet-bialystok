using AutoMapper;
using MediatR;
using Patronage.Api.Functions.Extensions;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;

namespace Patronage.Api.Functions.Queries.GetIssues
{
    public class GetIssuesListQueryHandler : IRequestHandler<GetIssuesListQuery, PageResult<IssueDto>>
    {
        private readonly IIssueService _issueService;
        private readonly IMapper _mapper;

        public GetIssuesListQueryHandler(IIssueService issueService, IMapper mapper)
        {
            _issueService = issueService;
            _mapper = mapper;
        }

        public Task<PageResult<IssueDto>> Handle(GetIssuesListQuery request, CancellationToken cancellationToken)
        {
            var baseQuery = _issueService.GetAllIssues();

            baseQuery = baseQuery.FilterBy(request);
            var totalItemCount = baseQuery.Count();

            var issues = baseQuery
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize);

            var issuesDto = _mapper.Map<List<IssueDto>>(issues);

            var result = new PageResult<IssueDto>(issuesDto, totalItemCount, request.PageSize, request.PageNumber);
            return Task.FromResult(result);
        }
    }
}
