using MediatR;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Commands.CreateIssue
{
    public class CreateIssueCommand : IRequest<int>
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public int BoardId { get; set; }
        public int StatusId { get; set; }
    }
}
