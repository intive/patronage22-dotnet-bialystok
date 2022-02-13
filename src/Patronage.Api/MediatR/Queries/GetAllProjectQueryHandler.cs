using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Queries
{
    public class GetAllProjectQueryHandler : IRequestHandler<GetAllProjectQuery, IEnumerable<ProjectDto>>
    {
        private readonly IProjectService _projectService;

        public GetAllProjectQueryHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public Task<IEnumerable<ProjectDto>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            
            return Task.FromResult(_projectService.GetAll());
        }
    }
}
