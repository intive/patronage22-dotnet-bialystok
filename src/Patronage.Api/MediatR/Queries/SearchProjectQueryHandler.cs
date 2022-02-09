using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Queries
{
    public class SearchProjectQueryHandler : IRequestHandler<SearchProjectQuery, int>
    {
        private readonly IProjectService _projectService;

        public SearchProjectQueryHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }
       
        Task<int> IRequestHandler<SearchProjectQuery, int>.Handle(SearchProjectQuery request, CancellationToken cancellationToken)
        {
            _projectService.GetById(request.Id);
            return Task.FromResult(request.Id);
        }
    }
}
