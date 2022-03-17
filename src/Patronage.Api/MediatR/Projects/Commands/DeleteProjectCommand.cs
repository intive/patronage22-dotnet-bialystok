using MediatR;

namespace Patronage.Api.MediatR.Projects.Commands
{
    public record DeleteProjectCommand(int id) : IRequest<bool>;
}