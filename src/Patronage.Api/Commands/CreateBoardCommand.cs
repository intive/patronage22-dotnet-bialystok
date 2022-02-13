using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.Commands
{
    public class CreateBoardCommand : IRequest<bool>
    {
        public BoardDto Data { get; set; }
    }
}
