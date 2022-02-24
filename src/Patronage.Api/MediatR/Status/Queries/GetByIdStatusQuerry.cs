using MediatR;

namespace Patronage.Api.MediatR.Status.Queries
{
    public record GetByIdStatusQuerry(int StatusId) : IRequest<IQueryable>;
}
