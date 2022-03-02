using MediatR;

namespace Patronage.Api.MediatR.Status.Commands
{
    public record DeleteStatusCommand(int StatusId) :IRequest<bool>;
    
}
