using MediatR;
using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Api.MediatR.Projects.Queries
{
    public record GetAllProjectsQuery(string? SearchedProject) : IRequest<IEnumerable<ProjectDto>>;
}
