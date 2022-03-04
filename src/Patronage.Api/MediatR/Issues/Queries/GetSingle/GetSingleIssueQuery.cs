using MediatR;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Queries.GetSingle
{
    public record GetSingleIssueQuery(int id) : IRequest<IssueDto>;
}
