using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Projects.Commands.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand>
    {
        private readonly IProjectService _projectService;

        public UpdateProjectHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }



        public Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            _projectService.Update(request.id, request.dto);

            return Task.FromResult(Unit.Value);
        }
    }
}
