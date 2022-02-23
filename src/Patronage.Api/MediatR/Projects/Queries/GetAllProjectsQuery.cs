using MediatR;
using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Api.MediatR.Projects.Queries
{
    public record GetAllProjectsQuery(string searchedProject) : IRequest<IEnumerable<ProjectDto>>;
}
