using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Status.Commands.Handlers
{
    public record CreateStatusHandler(string Code) : IRequest;
    
}
