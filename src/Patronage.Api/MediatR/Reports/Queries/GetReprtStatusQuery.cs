using MediatR;

namespace Patronage.Api.MediatR.Reports.Queries
{
    public record GetReprtStatusQuery(string reportId) : IRequest<string>;
}
