using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Commands
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
    {
        private readonly IProjectService _projectService;

        public DeleteProjectCommandHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            _projectService.Delete(request.Id);
            return Task.FromResult(Unit.Value);
        }
    }
}
