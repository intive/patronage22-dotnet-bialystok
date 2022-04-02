using Lucene.Net.Documents;
using Lucene.Net.Index;
using Patronage.Common;
using Patronage.Contracts.Helpers;

namespace Patronage.Contracts.ModelDtos.Boards
{
    public class UpdateBoardDto : IEntity
    {
        public string Alias { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int ProjectId { get; set; }

        public IEnumerable<Term> GetLuceneTerm()
        {
            return new List<Term>().Append(new Term(LuceneFieldNames.BoardName, Name));
        }

        public IEnumerable<TextField> GetLuceneTextField()
        {
            return new List<TextField>().Append(new TextField(LuceneFieldNames.BoardName, Name, Field.Store.YES));
        }
    }
}