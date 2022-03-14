using AutoMapper;
using MediatR;
using Patronage.Api.Exceptions;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Issues.Commands
{
    public class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueCommand, bool>
    {
        private readonly IIssueService _issueService;

        public UpdateIssueCommandHandler(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public async Task<bool> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
        {
            return await _issueService.UpdateAsync(request.Id, request.Dto);
        }
    }
}
