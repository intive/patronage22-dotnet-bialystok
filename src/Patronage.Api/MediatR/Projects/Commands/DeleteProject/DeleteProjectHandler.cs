using MediatR;
using Patronage.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.MediatR.Projects.Commands.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand>
    {
        private readonly IProjectService _projectService;

        public DeleteProjectHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }



        public Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            _projectService.Delete(request.id);

            return Task.FromResult(Unit.Value);
        }
    }
}
