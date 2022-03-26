using MediatR;
using Patronage.Contracts.ModelDtos.BoardsStatus;

namespace Patronage.Api.MediatR.BoardStatus.Commands
{
    public class CreateBoardStatusCommand : IRequest<bool>
    {
        public BoardStatusDto Dto { get; set; }

        public CreateBoardStatusCommand(BoardStatusDto dto)
        {
            Dto = dto;
        }
    }
}