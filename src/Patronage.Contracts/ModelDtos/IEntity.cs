using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace Patronage.Common
{
    public interface IEntity
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public IEnumerable<TextField> GetLuceneTextField();

        public IEnumerable<Term> GetLuceneTerm();
    }
}