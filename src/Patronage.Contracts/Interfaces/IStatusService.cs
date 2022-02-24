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
        IQueryable<StatusDto> GetAll();
        StatusDto GetById(int id);
        bool Create(string statusCode);
        bool Delete(int statusId);
        bool Update(int statusId, string statusCode);
    }
}
