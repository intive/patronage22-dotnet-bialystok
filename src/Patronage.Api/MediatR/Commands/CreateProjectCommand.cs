using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Commands
{
    public class CreateProjectCommand : IRequest<ProjectDto>
    {
        
            public ProjectDto Project { get; set; }
        
    }
}
