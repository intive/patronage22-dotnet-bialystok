using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Queries
{
    public class SearchProjectQuery : IRequest<ProjectDto>
    {
        public int Id { get; set; }
        public SearchProjectQuery(int id) 
        { Id = id; }
        public ProjectDto Project { get; set; }
    }

    public class SearchProjectQueryHandler : IRequestHandler<SearchProjectQuery, ProjectDto>
    {
        private readonly IProjectService _projectService;

        public SearchProjectQueryHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public Task<ProjectDto> Handle(SearchProjectQuery request, CancellationToken cancellationToken)
        {
            _projectService.Update(request.Id, request.Project);
            return Task.FromResult(request.Project);
        }
    }
}
