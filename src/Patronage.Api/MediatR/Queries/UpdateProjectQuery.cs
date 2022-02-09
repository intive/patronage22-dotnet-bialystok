using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Queries
{
    public class UpdateProjectQuery : IRequest<ProjectDto>
    {
        public int Id { get; set; }
        public UpdateProjectQuery(int id)
        { Id = id; }
        public ProjectDto Project { get; set; }
    }
}
