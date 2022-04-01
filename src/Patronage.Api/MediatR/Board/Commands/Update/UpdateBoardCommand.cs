using MediatR;
using Patronage.Contracts.ModelDtos.Boards;

namespace Patronage.Api.MediatR.Board.Commands.Update
{
    public class UpdateBoardCommand : IRequest<bool>
    {
        public UpdateBoardDto Data { get; set; } = null!;
        public int Id { get; set; }
    }
}