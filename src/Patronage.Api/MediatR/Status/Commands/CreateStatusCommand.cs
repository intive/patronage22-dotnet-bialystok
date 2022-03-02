using MediatR;

namespace Patronage.Api.MediatR.Status.Commands
{
    public record CreateStatusCommand(string Code) : IRequest<int>;
}
