using MediatR;
using Patronage.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.MediatR.Projects.Commands.CreateProject
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand>
    {
        private readonly IProjectService _projectService;

        public CreateProjectHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }



        public Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            _projectService.Create(request.dto);

            return Task.FromResult(Unit.Value);
        }
    }
}
