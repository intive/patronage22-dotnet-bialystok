using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Projects.Commands.Handlers
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, bool>
    {
        private readonly IProjectService _projectService;

        public DeleteProjectHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var isExistingRecord = await _projectService.Delete(request.id);

            return isExistingRecord;
        }
    }
}