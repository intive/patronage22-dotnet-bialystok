using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Board.Commands.Update
{
    public class UpdateBoardCommand : IRequest<bool>
    {
        public BoardDto Data { get; set; }
    }
}
