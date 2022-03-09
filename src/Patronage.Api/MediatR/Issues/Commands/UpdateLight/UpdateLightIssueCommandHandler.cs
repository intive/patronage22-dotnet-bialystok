using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Issues.Commands
{
    public class UpdateLightIssueCommandHandler : IRequestHandler<UpdateLightIssueCommand, bool>
    {
        private readonly IIssueService _issueService;

        public UpdateLightIssueCommandHandler(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public async Task<bool> Handle(UpdateLightIssueCommand request, CancellationToken cancellationToken)
        {
            return await _issueService.UpdateLightAsync(request.Id, request.Dto);
        }
    }
}