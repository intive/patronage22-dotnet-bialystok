using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.Interfaces
{
    public interface IEntityService<T>
    {
        Task<T> GetByIdAsync(int id);
    }
}
