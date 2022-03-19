using MediatR;
using Patronage.Contracts.ModelDtos.BoardsStatus;

namespace Patronage.Api.MediatR.BoardStatus.Commands
{
    public record CreateBoardStatusCommand(BoardStatusDto Dto) : IRequest<bool>;
}