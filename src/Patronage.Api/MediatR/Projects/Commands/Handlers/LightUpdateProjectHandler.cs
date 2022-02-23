using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Projects.Commands.Handlers
{
    public class LightUpdateProjectHandler : IRequestHandler<LightUpdateProjectCommand, bool>
    {
        private readonly IProjectService _projectService;

        public LightUpdateProjectHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<bool> Handle(LightUpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var isExistingRecord = await _projectService.LightUpdate(request.id, request.dto);

            return isExistingRecord;
        }
    }
}
