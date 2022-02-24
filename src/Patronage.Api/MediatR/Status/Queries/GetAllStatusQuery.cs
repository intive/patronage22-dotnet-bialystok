using MediatR;

namespace Patronage.Api.MediatR.Status.Queries
{
    public record GetAllStatusQuery() : IRequest<IQueryable>;
}
