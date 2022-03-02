using MediatR;
using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Api.MediatR.Projects.Queries
{
    public record GetSingleProjectQuery(int id) : IRequest<ProjectDto>;
}
