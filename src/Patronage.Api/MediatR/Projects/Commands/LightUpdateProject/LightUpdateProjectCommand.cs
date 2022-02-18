using MediatR;
using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Api.MediatR.Projects.Commands.LightUpdateProject
{
    public record LightUpdateProjectCommand(int id, PartialProjectDto dto) : IRequest;
}
