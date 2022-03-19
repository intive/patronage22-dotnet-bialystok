using MediatR;
using Patronage.Contracts.ModelDtos.Boards;

namespace Patronage.Api.MediatR.Board.Commands.Create
{
    public class CreateBoardCommand : IRequest<BoardDto>
    {
        public BaseBoardDto Data { get; set; } = null!;
    }
}