using Lucene.Net.Documents;
using Lucene.Net.Index;
using Patronage.Common;
using Patronage.Contracts.Helpers;

namespace Patronage.Contracts.ModelDtos.Projects
{
    public class UpdateProjectDto : IEntity
    {
        public string Alias { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<TextField> GetLuceneTextField()
        {
            return new List<TextField>().Append(new TextField(LuceneFieldNames.ProjectName, Name, Field.Store.YES));
        }

        public IEnumerable<Term> GetLuceneTerm()
        {
            return new List<Term>().Append(new Term(LuceneFieldNames.ProjectName, Name));
        }
    }
}