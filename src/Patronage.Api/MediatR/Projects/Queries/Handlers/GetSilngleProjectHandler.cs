using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Api.MediatR.Projects.Queries.Handlers
{
    public class GetSilngleProjectHandler : IRequestHandler<GetSingleProjectQuery, ProjectDto?>
    {
        private readonly IProjectService _projectService;

        public GetSilngleProjectHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<ProjectDto?> Handle(GetSingleProjectQuery request, CancellationToken cancellationToken)
        {
            return await _projectService.GetById(request.id);
        }
    }
}