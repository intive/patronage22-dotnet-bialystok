using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Boards;

namespace Patronage.Api.MediatR.Board.Commands.Create
{
    public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, BoardDto?>
    {
        public readonly IBoardService _boardService;
        private readonly ILuceneService _luceneService;

        public CreateBoardCommandHandler(IBoardService boardService, ILuceneService luceneService)
        {
            _boardService = boardService;
            _luceneService = luceneService;
        }

        public async Task<BoardDto?> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
        {
            _luceneService.AddDocument(request.Data);
            return await _boardService.CreateBoardAsync(request.Data);
        }
    }
}