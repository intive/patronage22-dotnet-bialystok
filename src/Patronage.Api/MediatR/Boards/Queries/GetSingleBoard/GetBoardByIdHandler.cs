using MediatR;
using Patronage.Contracts.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.MediatR.Boards.Queries
{
    public record GetBoardByIdHandler : IRequestHandler<GetAllBoardsByIdQuery, ProjectDto>
    {
        public Task<ProjectDto> Handle(GetAllBoardsByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
