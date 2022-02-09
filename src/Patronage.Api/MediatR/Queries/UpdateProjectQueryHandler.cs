using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Queries
{
   

    public class UpdateProjectQueryHandler : IRequestHandler<UpdateProjectQuery, ProjectDto>
    {
        private readonly IProjectService _projectService;

        public UpdateProjectQueryHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public Task<ProjectDto> Handle(UpdateProjectQuery request, CancellationToken cancellationToken)
        {
            _projectService.Update(request.Id, request.Project);
            return Task.FromResult(request.Project);
        }
    }
}
