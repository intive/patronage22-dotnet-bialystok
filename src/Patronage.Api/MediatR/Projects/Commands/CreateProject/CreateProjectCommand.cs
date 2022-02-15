using MediatR;
using Patronage.Contracts.ModelDtos.Projects;

namespace Patronage.Api.MediatR.Projects.Commands.CreateProject
{
    public record CreateProjectCommand(CreateOrUpdateProjectDto dto) : IRequest;
}
