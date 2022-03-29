using Patronage.Common;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Contracts.Interfaces
{
    public interface ILuceneService
    {
        public FilteredEntities Search(string name, string description);

        public void DeleteDocument(IEntity entity);

        public void AddDocument(IEntity entity);
    }
}