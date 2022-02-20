using Patronage.Contracts.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.Interfaces
{
    public interface IBoardStatusService
    {
        IEnumerable<BoardStatusDto> GetAll();
        IEnumerable<BoardStatusDto> GetById(int boardId, int statusId);
        public BoardStatusDto Create(BoardStatusDto dto);
        public void Delete(int boardId, int statusId);

    }
}
