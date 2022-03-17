using MediatR;

namespace Patronage.Api.MediatR.Status.Commands
{
    public record UpdateStatusCommand(int Id, string Status) : IRequest<bool>;
}