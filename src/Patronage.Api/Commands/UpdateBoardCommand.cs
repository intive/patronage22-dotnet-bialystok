﻿using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.Commands
{
    public class UpdateBoardCommand : IRequest<bool>
    {
        public BoardDto Data { get; set; }
    }
}
