using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Api.MediatR.Projects.Queries.GetAllProjects
{
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, IEnumerable<ProjectDto>>
    {
        private readonly IProjectService _projectService;

        public GetAllProjectsHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public Task<IEnumerable<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_projectService.GetAll(request.searchedProject));
        }
    }
}
