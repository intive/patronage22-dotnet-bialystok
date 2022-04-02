using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Board.Commands.Update
{
    public class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, bool>
    {
        public readonly IBoardService boardService;
        private readonly ILuceneService _luceneService;

        public UpdateBoardCommandHandler(IBoardService boardService, ILuceneService luceneService)
        {
            this.boardService = boardService;
            _luceneService = luceneService;
        }

        public async Task<bool> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
        {
            var result = await boardService.UpdateBoardAsync(request.Data, request.Id);
            if (result)
            {
                _luceneService.UpdateDocument(request.Data, request.Id);
            }
            return result;
        }
    }
}