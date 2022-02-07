using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Commands
{
    public class CreateProjectCommand : IRequest<ProjectDto>
    {
        public ProjectDto Project { get; set; }
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
    {
        private readonly IProjectService _projectService;

        public CreateProjectCommandHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            _projectService.Create(request.Project);

            return Task.FromResult(request.Project);
        }

    }
}
