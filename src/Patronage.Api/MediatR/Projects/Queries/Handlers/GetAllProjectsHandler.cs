using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Api.MediatR.Projects.Queries.Handlers
{
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
    {
        private readonly IProjectService _projectService;

        public GetAllProjectsHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            return await _projectService.GetAll(request.SearchedProject);
        }
    }
}