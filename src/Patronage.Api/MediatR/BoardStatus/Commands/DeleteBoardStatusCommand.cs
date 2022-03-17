using MediatR;

namespace Patronage.Api.MediatR.BoardStatus.Commands
{
    public record DeleteBoardStatusCommand(int boardId, int statusId) : IRequest<bool>;
}