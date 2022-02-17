using MediatR;
using Patronage.Api.MediatR.Issues.Commands.CreateIssue;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Projects.Commands.CreateProject
{
    public class CreateIssueHandler : IRequestHandler<CreateIssueCommand>
    {
        private readonly IIssueService _issueService;

        public CreateIssueHandler(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public Task<Unit> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            _issueService.Create(request.dto);

            return Task.FromResult(Unit.Value);
        }
    }
}
