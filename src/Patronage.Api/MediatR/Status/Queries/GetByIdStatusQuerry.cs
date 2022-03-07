using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Status.Queries
{
    public record GetByIdStatusQuerry(int StatusId) : IRequest<StatusDto>;
}
