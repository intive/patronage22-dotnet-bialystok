using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Projects.Commands.Handlers
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, bool>
    {
        private readonly IProjectService _projectService;
        private readonly ILuceneService _luceneService;

        public UpdateProjectHandler(IProjectService projectService, ILuceneService luceneService)
        {
            _projectService = projectService;
            _luceneService = luceneService;
        }

        public async Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var isExistingRecord = await _projectService.Update(request.id, request.dto);

            if (isExistingRecord)
            {
                _luceneService.UpdateDocument(request.dto, request.id);
            }

            return isExistingRecord;
        }
    }
}