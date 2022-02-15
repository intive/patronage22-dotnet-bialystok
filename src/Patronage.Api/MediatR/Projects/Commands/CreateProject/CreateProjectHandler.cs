using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Projects.Commands.CreateProject
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand>
    {
        private readonly IProjectService _projectService;

        public CreateProjectHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }



        public Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            _projectService.Create(request.dto);

            return Task.FromResult(Unit.Value);
        }
    }
}
