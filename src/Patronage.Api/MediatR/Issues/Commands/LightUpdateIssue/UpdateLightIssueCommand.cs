using MediatR;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Commands.LightUpdateIssue
{
    public class UpdateLightIssueCommand : IRequest
    {
        public int Id { get; set; }
        public PartialIssueDto Dto { get; set; }
    }
}
