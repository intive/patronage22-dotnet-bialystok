using AutoMapper;
using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Issues.Commands.UpdateIssue
{
    public class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueCommand, Unit>
    {
        private readonly IIssueService _issueService;
        private readonly IMapper _mapper;

        public UpdateIssueCommandHandler(IIssueService issueService, IMapper mapper)
        {
            _issueService = issueService;
            _mapper = mapper;
        }

        public Task<Unit> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
        {


            return Task.FromResult(Unit.Value);
        }
    }
}
