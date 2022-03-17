using MediatR;
using Patronage.Contracts.ModelDtos.Issues;

namespace Patronage.Api.MediatR.Issues.Commands
{
    public class UpdateLightIssueCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public PartialIssueDto Dto { get; set; }

        public UpdateLightIssueCommand(int id, PartialIssueDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}