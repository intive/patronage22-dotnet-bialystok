using AutoMapper;
using MediatR;
using Patronage.Api.Queries;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.Handlers.Handlers.Queries
{
    public class GetBoardsHandler : IRequestHandler<GetBoardsQuery, IEnumerable<BoardDto>>
    {
        private readonly IBoardService boardService;

        public GetBoardsHandler(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public Task<IEnumerable<BoardDto>> Handle(GetBoardsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(boardService.GetBoards(request.filter));
        }
    }
}
