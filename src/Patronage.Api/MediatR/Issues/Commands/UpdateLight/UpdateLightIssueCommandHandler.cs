using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Commands
{
    public class UpdateLightIssueCommandHandler : IRequestHandler<UpdateLightIssueCommand, bool>
    {
        private readonly IIssueService _issueService;
        private readonly ILuceneService _luceneService;

        public UpdateLightIssueCommandHandler(IIssueService issueService, ILuceneService luceneService)
        {
            _issueService = issueService;
            _luceneService = luceneService;
        }

        public async Task<bool> Handle(UpdateLightIssueCommand request, CancellationToken cancellationToken)
        {
            var result = await _issueService.UpdateLightAsync(request.Id, request.Dto);

            if (result && request.Dto.Name is not null)
            {
                _luceneService.UpdateDocument(new BaseIssueDto
                {
                    Name = request.Dto.Name!.Data!
                }, request.Id);
            }

            return result;
        }
    }
}