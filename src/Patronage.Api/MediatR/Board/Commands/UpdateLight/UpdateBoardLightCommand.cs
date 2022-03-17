using MediatR;
using Patronage.Contracts.ModelDtos.Board;

namespace Patronage.Api.MediatR.Board.Commands.UpdateLight
{
    public class UpdateBoardLightCommand : IRequest<bool>
    {
        public PartialBoardDto Data { get; set; } = null!;
        public int Id { get; set; }
    }
}