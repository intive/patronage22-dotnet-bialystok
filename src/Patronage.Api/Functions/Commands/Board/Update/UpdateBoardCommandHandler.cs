﻿using AutoMapper;
using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.Functions.Commands.Board.Update
{
    public class UpdateBoardCommandHandler : IRequestHandler<UpdateBoardCommand, bool>
    {
        public readonly IBoardService boardService;

        public UpdateBoardCommandHandler(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public async Task<bool> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
        {
            return await boardService.UpdateBoardAsync(request.Data);
        }
    }
}
