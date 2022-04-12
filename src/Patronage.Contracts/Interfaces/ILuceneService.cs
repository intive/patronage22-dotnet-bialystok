using Patronage.Common;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Contracts.Interfaces
{
    public interface ILuceneService
    {
        public FilteredEntities Search(string? name = null, string? description = null);

        public void AddDocument(IEntity entity);

        public void UpdateDocument(IEntity entity, int id);

        public void DeleteDocument(IEntity entity);
    }
}