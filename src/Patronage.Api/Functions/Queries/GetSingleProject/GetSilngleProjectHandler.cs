using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Api.Functions.Queries.GetSingleProject
{
    public class GetSilngleProjectHandler : IRequestHandler<GetSingleProjectQuery, ProjectDto>
    {
        private readonly IProjectService _projectService;

        public GetSilngleProjectHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public Task<ProjectDto> Handle(GetSingleProjectQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_projectService.GetById(request.id));
        }
    }
}
