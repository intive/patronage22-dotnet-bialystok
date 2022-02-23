using MediatR;
using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Api.MediatR.Projects.Commands
{
    public record CreateProjectCommand(CreateProjectDto dto) : IRequest<int>;
}
