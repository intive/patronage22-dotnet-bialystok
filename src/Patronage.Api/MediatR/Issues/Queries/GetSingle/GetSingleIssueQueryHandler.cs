﻿using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Queries
{
    public class GetSingleIssueQueryHandler : IRequestHandler<GetSingleIssueQuery, IssueDto?>
    {
        private readonly IIssueService _issueService;

        public GetSingleIssueQueryHandler(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public async Task<IssueDto?> Handle(GetSingleIssueQuery request, CancellationToken cancellationToken)
        {
            var result = await _issueService.GetByIdAsync(request.id);
            if (result == null || !result.IsActive)
            {
                return null;
            }

            var issueDto = new IssueDto(result);

            return issueDto;
        }
    }
}