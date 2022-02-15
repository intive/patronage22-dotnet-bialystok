using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.Functions.Commands.Board.UpdateLight
{
    public class UpdateBoardLightCommand : IRequest<bool>
    {
        public PartialBoardDto Data { get; set; }
    }
}
