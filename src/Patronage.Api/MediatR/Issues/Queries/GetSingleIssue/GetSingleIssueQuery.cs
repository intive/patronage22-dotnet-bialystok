using MediatR;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Queries.GetSingleIssue
{
    public record GetSingleIssueQuery(int id) : IRequest<IssueDto>;
}
