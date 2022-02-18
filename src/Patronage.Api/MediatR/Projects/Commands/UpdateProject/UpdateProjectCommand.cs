using MediatR;
using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Api.MediatR.Projects.Commands.UpdateProject
{
    public record UpdateProjectCommand(int id, CreateOrUpdateProjectDto dto) : IRequest;
}
