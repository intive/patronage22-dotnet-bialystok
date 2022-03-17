using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Status.Queries
{
    public record GetAllStatusQuery() : IRequest<IEnumerable<StatusDto>>;
}