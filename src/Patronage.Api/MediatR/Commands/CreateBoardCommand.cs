using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Commands
{
    public class CreateBoardCommand : IRequest<bool>
    {
        public BoardDto Data { get; set; }
    }
}
