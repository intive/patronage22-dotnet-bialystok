﻿using MediatR;
using Patronage.Contracts.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.MediatR.BoardStatus.Queries
{
    public record GetByIdBoardStatusQuery(int BoardId, int StatusId) : IRequest<IEnumerable<BoardStatusDto>>;
}