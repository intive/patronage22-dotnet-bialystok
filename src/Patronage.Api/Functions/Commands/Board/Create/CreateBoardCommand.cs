using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.Functions.Commands.Board.Create
{
    public class CreateBoardCommand : IRequest<BoardDto>
    {
        public BoardDto Data { get; set; }
    }
}
