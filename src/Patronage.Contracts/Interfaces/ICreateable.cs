using Patronage.Contracts.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Models.Interfaces
{
    public interface ICreateable
    {
        DateTime CreatedOn(BoardDto board);
    }
}
