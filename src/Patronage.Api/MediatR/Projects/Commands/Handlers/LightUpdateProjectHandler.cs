using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Api.MediatR.Projects.Commands.Handlers
{
    public class LightUpdateProjectHandler : IRequestHandler<LightUpdateProjectCommand, bool>
    {
        private readonly IProjectService _projectService;
        private readonly ILuceneService _luceneService;

        public LightUpdateProjectHandler(IProjectService projectService, ILuceneService luceneService)
        {
            _projectService = projectService;
            _luceneService = luceneService;
        }

        public async Task<bool> Handle(LightUpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var isExistingRecord = await _projectService.LightUpdate(request.id, request.dto);

            if (isExistingRecord && request.dto.Name?.Data is not null)
            {
                _luceneService.UpdateDocument(new ProjectDto
                {
                    Name = request.dto.Name!.Data!
                }, request.id);
            }

            return isExistingRecord;
        }
    }
}