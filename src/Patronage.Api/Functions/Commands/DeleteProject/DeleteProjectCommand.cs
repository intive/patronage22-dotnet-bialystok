using MediatR;

namespace Patronage.Api.Functions.Commands.DeleteProject
{
    public record DeleteProjectCommand(int id) : IRequest;
}
