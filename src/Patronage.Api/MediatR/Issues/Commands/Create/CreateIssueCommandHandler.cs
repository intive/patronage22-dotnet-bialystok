using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Commands.Create
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
            return await _issueService.CreateAsync(new BaseIssueDto(request.Data));
        }
    }
}
