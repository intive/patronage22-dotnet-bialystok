using MediatR;
using Patronage.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.Functions.Commands.LightUpdateProject
{
    public class LightUpdateProjectHandler : IRequestHandler<LightUpdateProjectCommand>
    {
        private readonly IProjectService _projectService;

        public LightUpdateProjectHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public Task<Unit> Handle(LightUpdateProjectCommand request, CancellationToken cancellationToken)
        {
            _projectService.LightUpdate(request.id, request.dto);

            return Task.FromResult(Unit.Value);
        }
    }
}
