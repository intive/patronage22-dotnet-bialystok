using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.MediatR.Boards.Commands.DeleteBoard
{
    public record DeleteBoardCommand(int id) : IRequest;
}
