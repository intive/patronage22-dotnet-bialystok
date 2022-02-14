using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Commands
{
    public class UpdateBoardLightCommand : IRequest<bool>
    {
        public PartialBoardDto Data { get; set; }
    }
}
