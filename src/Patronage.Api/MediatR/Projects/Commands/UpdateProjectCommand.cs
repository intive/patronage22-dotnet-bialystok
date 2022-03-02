using MediatR;
using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Api.MediatR.Projects.Commands
{
    public record UpdateProjectCommand(int id, UpdateProjectDto dto) : IRequest<bool>;
}
