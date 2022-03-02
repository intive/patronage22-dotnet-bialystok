using Patronage.Contracts.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.Interfaces
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusDto>> GetAll();
        Task <StatusDto> GetById(int id);
        Task<int> Create(string statusCode);
        Task<bool> Delete(int statusId);
        Task<bool> Update(int statusId, string statusCode);
    }
}
