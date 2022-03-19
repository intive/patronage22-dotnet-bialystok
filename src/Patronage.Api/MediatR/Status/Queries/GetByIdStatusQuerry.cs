using MediatR;
using Patronage.Contracts.ModelDtos.Statuses;

namespace Patronage.Api.MediatR.Status.Queries
{
    public record GetByIdStatusQuerry(int StatusId) : IRequest<StatusDto>;
}