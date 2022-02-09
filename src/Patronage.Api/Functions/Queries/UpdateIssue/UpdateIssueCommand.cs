using MediatR;

namespace Patronage.Api.Functions.Queries.UpdateIssue
{
    public class UpdateIssueCommand : IRequest
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public int? BoardId { get; set; }
        public int StatusId { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
