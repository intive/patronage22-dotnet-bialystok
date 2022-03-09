using MediatR;

namespace Patronage.Api.MediatR.Issues.Commands
{
    public class DeleteIssueCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteIssueCommand(int id)
        {
            Id = id;
        }
    }
}
