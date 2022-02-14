using MediatR;
using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Api.Functions.Queries.GetAllProjects
{
    public record GetAllProjectsQuery(string searchedProject) : IRequest<IEnumerable<ProjectDto>>;
}
