using MediatR;

namespace Patronage.Api.MediatR.Status.Queries.Handlers
{
    public record GetAllStatusHandler() :IRequest<IQueryable>;
}
