using AutoMapper;
using Patronage.Contracts;
using Patronage.Contracts.ModelDtos.Projects;
using Patronage.Contracts.ModelDtos;
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

            CreateMap<Issue, IssueDto>()
                .ForMember(m => m.Alias.value, c => c.MapFrom(s => s.Alias))
                .ForMember(m => m.Name.value, c => c.MapFrom(s => s.Name))
                .ForMember(m => m.Description.value, c => c.MapFrom(s => s.Description));
            CreateMap<BaseIssueDto, Issue>();

            CreateMap<Board, BoardDto>().ReverseMap();

            CreateMap<PartialBoardDto, Board>()               
                .ForMember(x => x.Name, y => { y.Condition(src => src.Name != null); y.MapFrom(z => z.Name.value); })
                .ForMember(x => x.Description, y => { y.Condition(src => src.Description != null); y.MapFrom(z => z.Description.value); })
                .ForMember(x => x.IsActive, y => { y.Condition(src => src.IsActive != null); y.MapFrom(z => z.IsActive.value); });           
        }
    }
}
