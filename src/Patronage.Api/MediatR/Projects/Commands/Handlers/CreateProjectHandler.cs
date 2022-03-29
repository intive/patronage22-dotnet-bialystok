using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Projects.Commands.Handlers
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, int>
    {
        private readonly IProjectService _projectService;
        private readonly ILuceneService _luceneService;

        public CreateProjectHandler(IProjectService projectService, ILuceneService luceneService)
        {
            _projectService = projectService;
            _luceneService = luceneService;
        }

        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            _luceneService.AddDocument(request.dto);
            return await _projectService.Create(request.dto);
        }
    }
}