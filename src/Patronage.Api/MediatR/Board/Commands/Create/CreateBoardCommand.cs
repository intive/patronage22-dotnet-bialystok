using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Board.Commands.Create
{
    public class CreateBoardCommand : IRequest<BoardDto>
    {
        public BoardDto Data { get; set; } = null!;
    }
}
