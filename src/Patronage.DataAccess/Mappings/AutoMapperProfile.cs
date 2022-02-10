using AutoMapper;
using Patronage.Contracts;
using Patronage.Contracts.ModelDtos.Projects;
using Patronage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.DataAccess.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<CreateOrUpdateProjectDto, Project>();
            CreateMap<PartialProjectDto, Project>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
