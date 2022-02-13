using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Commands
{
    public class DeleteProjectCommand : IRequest
    {
        public int Id { get; set; }
        public DeleteProjectCommand(int id)
        { Id = id; }
      
    }
}
