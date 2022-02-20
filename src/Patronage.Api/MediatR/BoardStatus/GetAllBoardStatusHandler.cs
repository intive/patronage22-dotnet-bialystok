using MediatR;
using Patronage.Api.MediatR.BoardStatus.Queries;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.MediatR.BoardStatus
{
    public class GetAllBoardStatusHandler : IRequestHandler<GetAllBoardStatusQuery, IEnumerable<BoardStatusDto>>
    {
        private readonly IBoardStatusService _boardStatusService;

        public GetAllBoardStatusHandler(IBoardStatusService boardStatusService)
        {
            _boardStatusService = boardStatusService;
        }

        public Task<IEnumerable<BoardStatusDto>> Handle(GetAllBoardStatusQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_boardStatusService.GetAll()); 
        }
    }
}
