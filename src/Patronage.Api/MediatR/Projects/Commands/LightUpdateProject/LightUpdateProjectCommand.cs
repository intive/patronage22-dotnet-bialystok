using MediatR;
using Patronage.Contracts.ModelDtos.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.MediatR.Projects.Commands.LightUpdateProject
{
    public record LightUpdateProjectCommand(int id, PartialProjectDto dto) : IRequest;
}
