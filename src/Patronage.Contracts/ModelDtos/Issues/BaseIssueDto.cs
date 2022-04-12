using Lucene.Net.Documents;
using Lucene.Net.Index;
using Patronage.Common;
using Patronage.Contracts.Helpers;
using Patronage.Models;

namespace Patronage.Contracts.ModelDtos.Issues
{
    public class BaseIssueDto : IEntity, IEquatable<BaseIssueDto>
    {
        public string Alias { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public int? BoardId { get; set; }
        public int StatusId { get; set; }
        public string? AssignUserId { get; set; }
        public DateTime CreatedOn { get; set; }

        public BaseIssueDto(Issue issue)
        {
            Alias = issue.Alias;
            Name = issue.Name;
            Description = issue.Description;
            ProjectId = issue.ProjectId;
            BoardId = issue.BoardId;
            StatusId = issue.StatusId;
            AssignUserId = issue.AssignUserId;
            CreatedOn = issue.CreatedOn;
        }

        public BaseIssueDto()
        {
        }

        public IEnumerable<TextField> GetLuceneTextField()
        {
            var list = new List<TextField>().Append(new TextField(LuceneFieldNames.IssueName, Name, Field.Store.YES));
            return list.Append(new TextField(LuceneFieldNames.IssueDescription, Description, Field.Store.YES));
        }

        public IEnumerable<Term> GetLuceneTerm()
        {
            var list = new List<Term>().Append(new Term(LuceneFieldNames.IssueName, Name));
            return list.Append(new Term(LuceneFieldNames.IssueDescription, Description));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description);
        }

        public bool Equals(BaseIssueDto? other)
        {
            if (Object.ReferenceEquals(other, null)) return false;

            if (Object.ReferenceEquals(this, other)) return true;

            return (Description?.Equals(other.Description) ?? (Description is null && other.Description is null)) && Name.Equals(other.Name);
        }
    }
}