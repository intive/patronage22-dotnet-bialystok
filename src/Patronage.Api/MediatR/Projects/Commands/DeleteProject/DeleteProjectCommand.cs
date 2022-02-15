using MediatR;

namespace Patronage.Api.MediatR.Projects.Commands.DeleteProject
{
    public record DeleteProjectCommand(int id) : IRequest;
}
