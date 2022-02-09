using AutoMapper;
using MediatR;
using Patronage.Api.Functions.Extensions;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.Functions.Queries.GetIssues
{
    public class GetIssuesListQueryHandler : IRequestHandler<GetIssuesListQuery, List<IssueDto>>
    {
        private readonly IIssueService _issueService;
        private readonly IMapper _mapper;

        public GetIssuesListQueryHandler(IIssueService issueService, IMapper mapper)
        {
            _issueService = issueService;
            _mapper = mapper;
        }

        public Task<List<IssueDto>> Handle(GetIssuesListQuery request, CancellationToken cancellationToken)
        {
            var issues = _issueService.GetAllIssues();

            issues = issues.FilterBy(request);
            issues = issues
                .Skip(request.PageSize * (request.PageNumber - 1))
                .Take(request.PageSize);

            var issueDto = _mapper.Map<List<IssueDto>>(issues);

            return Task.FromResult(issueDto);
        }
    }
}
