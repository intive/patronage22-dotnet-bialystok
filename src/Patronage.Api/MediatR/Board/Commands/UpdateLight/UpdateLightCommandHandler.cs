using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Boards;

namespace Patronage.Api.MediatR.Board.Commands.UpdateLight
{
    public class UpdateLightCommandHandler : IRequestHandler<UpdateBoardLightCommand, bool>
    {
        public readonly IBoardService boardService;
        private readonly ILuceneService _luceneService;

        public UpdateLightCommandHandler(IBoardService boardService, ILuceneService luceneService)
        {
            this.boardService = boardService;
            _luceneService = luceneService;
        }

        public async Task<bool> Handle(UpdateBoardLightCommand request, CancellationToken cancellationToken)
        {
            var result = await boardService.UpdateBoardLightAsync(request.Data, request.Id);

            if (result && request.Data.Name is not null)
            {
                _luceneService.UpdateDocument(new UpdateBoardDto
                {
                    Name = request.Data.Name!.Data!
                }, request.Id);
            }

            return result;
        }
    }
}