using MediatR;
using Patronage.Contracts.ModelDtos.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.Functions.Commands.UpdateProject
{
    public record UpdateProjectCommand(int id, CreateOrUpdateProjectDto dto) : IRequest;
}
