using MediatR;
using Patronage.Api.MediatR.BoardStatus.Commands;
using Patronage.Api.MediatR.BoardStatus.Queries;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.MediatR.BoardStatus.Commands.Handlers
{
    public class CreateBoardStatusHandler : IRequestHandler<CreateBoardStatusCommand, bool>
    {
        private readonly IBoardStatusService _boardStatusService;

        public CreateBoardStatusHandler(IBoardStatusService boardStatusService)
        {
            _boardStatusService = boardStatusService;
        }

        public Task<bool> Handle(CreateBoardStatusCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_boardStatusService.Create(request.Dto));
        }
    }
}
