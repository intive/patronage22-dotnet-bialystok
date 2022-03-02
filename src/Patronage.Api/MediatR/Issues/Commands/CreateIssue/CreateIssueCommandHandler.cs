using MediatR;
using Patronage.Api.MediatR.Issues.Commands.CreateIssue;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Issues;
using Patronage.Models;

namespace Patronage.Api.MediatR.Projects.Commands.CreateProject
{
    public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, IssueDto?>
    {
        private readonly IIssueService _issueService;

        public CreateIssueCommandHandler(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public async Task<IssueDto?> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            return await _issueService.CreateAsync(request.Data);
        }
    }
}
