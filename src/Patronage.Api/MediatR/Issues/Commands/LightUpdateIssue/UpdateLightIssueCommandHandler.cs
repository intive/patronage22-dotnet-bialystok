using AutoMapper;
using MediatR;
using Patronage.Api.Exceptions;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Issues.Commands.LightUpdateIssue
{
    public class UpdateLightIssueCommandHandler : IRequestHandler<UpdateLightIssueCommand, Unit>
    {
        private readonly IIssueService _issueService;

        public UpdateLightIssueCommandHandler(IIssueService issueService)
        {
            _issueService = issueService;
        }

        public Task<Unit> Handle(UpdateLightIssueCommand request, CancellationToken cancellationToken)
        {
            var issue = _issueService.GetById(request.Id);

            if (issue == null)
            {
                throw new NotFoundException("Issue not found");
            }

            issue.Alias = request.Dto.Alias?.Data ?? issue.Alias;
            issue.Name = request.Dto.Name?.Data ?? issue.Name;
            issue.Description = request.Dto.Description?.Data ?? issue.Description;
            issue.ProjectId = request.Dto.ProjectId?.Data ?? issue.ProjectId;
            issue.BoardId = request.Dto.BoardId?.Data ?? issue.BoardId;
            issue.StatusId = request.Dto.StatusId?.Data ?? issue.StatusId;
            issue.IsActive = request.Dto.IsActive?.Data ?? issue.IsActive;

            _issueService.Save();

            return Task.FromResult(Unit.Value);
        }
    }
}