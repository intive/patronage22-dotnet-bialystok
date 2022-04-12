using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Commands
{
    public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, IssueDto?>
    {
        private readonly IIssueService _issueService;
        private readonly ILuceneService _luceneService;

        public CreateIssueCommandHandler(IIssueService issueService, ILuceneService luceneService)
        {
            _issueService = issueService;
            _luceneService = luceneService;
        }

        public async Task<IssueDto?> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            var result = await _issueService.CreateAsync(request.Data);
            if (result is not null)
            {
                _luceneService.AddDocument(request.Data);
            }
            return result;
        }
    }
}