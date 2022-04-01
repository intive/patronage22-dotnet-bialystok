using Patronage.Contracts.ModelDtos.Statuses;

namespace Patronage.Contracts.Interfaces
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusDto>> GetAll();

        Task<StatusDto?> GetById(int id);

        Task<int> Create(string statusCode);

        Task<bool> Delete(int statusId);

        Task<bool> Update(int statusId, string statusCode);
    }
}