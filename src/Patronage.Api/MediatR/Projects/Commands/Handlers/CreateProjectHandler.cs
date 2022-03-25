using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Projects.Commands.Handlers
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IProjectService _projectService;

        public CreateProjectHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            return await _projectService.Create(request.dto);
        }
    }
}