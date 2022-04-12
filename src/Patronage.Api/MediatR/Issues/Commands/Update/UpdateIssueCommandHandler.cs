using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Issues.Commands
{
    public class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueCommand, bool>
    {
        private readonly IIssueService _issueService;
        private readonly ILuceneService _luceneService;

        public UpdateIssueCommandHandler(IIssueService issueService, ILuceneService luceneService)
        {
            _issueService = issueService;
            _luceneService = luceneService;
        }

        public async Task<bool> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
        {
            var result = await _issueService.UpdateAsync(request.Id, request.Dto);
            if (result)
            {
                _luceneService.UpdateDocument(request.Dto, request.Id);
            }
            return result;
        }
    }
}