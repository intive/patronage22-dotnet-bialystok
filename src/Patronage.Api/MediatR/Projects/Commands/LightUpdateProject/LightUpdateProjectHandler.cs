using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Projects.Commands.LightUpdateProject
{
    public class LightUpdateProjectHandler : IRequestHandler<LightUpdateProjectCommand>
    {
        private readonly IProjectService _projectService;

        public LightUpdateProjectHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public Task<Unit> Handle(LightUpdateProjectCommand request, CancellationToken cancellationToken)
        {
            _projectService.LightUpdate(request.id, request.dto);

            return Task.FromResult(Unit.Value);
        }
    }
}
