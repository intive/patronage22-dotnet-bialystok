using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;


namespace Patronage.Api.MediatR.Boards.Queries
{
    public class GetAllBoardsHandler : IRequestHandler<GetAllBoardsQuery, IEnumerable<BoardDto>>
    {
        private readonly IBoardService _boardService;

        public GetAllBoardsHandler(IBoardService boardService)
        {
            _boardService = boardService;
        }



        public Task<IEnumerable<BoardDto>> Handle(GetAllBoardsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_boardService.GetBoards(request.filterBoard));
        }
    }
}
