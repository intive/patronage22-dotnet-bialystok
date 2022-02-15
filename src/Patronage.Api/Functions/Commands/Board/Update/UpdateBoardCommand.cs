using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.Functions.Commands.Board.Update
{
    public class UpdateBoardCommand : IRequest<bool>
    {
        public BoardDto Data { get; set; }
    }
}
