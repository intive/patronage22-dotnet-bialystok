using MediatR;
using Patronage.Contracts.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.MediatR.Boards.Queries
{
    public class GetAllBoardsHandler : IRequestHandler<GetAllBoardsQuery, List<ProjectDto>>
    {
        public Task<List<ProjectDto>> Handle(GetAllBoardsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
