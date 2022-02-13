using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Queries
{
    public class GetAllProjectQuery : IRequest<IEnumerable<ProjectDto>>
    {
       
    }
}
