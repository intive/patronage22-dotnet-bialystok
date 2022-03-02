using MediatR;
using Patronage.Contracts.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.MediatR.BoardStatus.Commands
{
    public record DeleteBoardStatusCommand(int boardId, int statusId) : IRequest<bool>;
}
