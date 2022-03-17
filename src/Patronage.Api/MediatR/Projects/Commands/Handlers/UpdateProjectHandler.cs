using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Projects.Commands.Handlers
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, bool>
    {
        private readonly IProjectService _projectService;

        public UpdateProjectHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var isExistingRecord = await _projectService.Update(request.id, request.dto);

            return isExistingRecord;
        }
    }
}