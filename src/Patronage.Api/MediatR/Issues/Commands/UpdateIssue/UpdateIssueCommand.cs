using MediatR;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Commands.UpdateIssue
{
    public class UpdateIssueCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public BaseIssueDto Dto { get; set; }

        public UpdateIssueCommand(int id, BaseIssueDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
