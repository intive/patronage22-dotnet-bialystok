using AutoMapper;
using Patronage.Contracts;
using Patronage.Contracts.ModelDtos.Projects;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;

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

            /*CreateMap<Issue, IssueDto>()
                .ForMember(m => m.Alias.Data, c => c.MapFrom(s => s.Alias))
                .ForMember(m => m.Name.Data, c => c.MapFrom(s => s.Name))
                .ForMember(m => m.Description.Data, c => c.MapFrom(s => s.Description));*/
            CreateMap<BaseIssueDto, Issue>();

            CreateMap<Issue, IssueDto>();

            CreateMap<Board, BoardDto>().ReverseMap();

            CreateMap<PartialBoardDto, Board>()         
                .ForMember(x => x.Alias, y => { y.Condition(src => src.Alias != null); y.MapFrom(z => z.Alias.Data); })
                .ForMember(x => x.Name, y => { y.Condition(src => src.Name != null); y.MapFrom(z => z.Name.Data); })
                .ForMember(x => x.Description, y => { y.Condition(src => src.Description != null); y.MapFrom(z => z.Description.Data); })
                .ForMember(x => x.ProjectId, y => { y.Condition(src => src.ProjectId != null); y.MapFrom(z => z.ProjectId.Data); })
                .ForMember(x => x.IsActive, y => { y.Condition(src => src.IsActive != null); y.MapFrom(z => z.IsActive.Data); });           
        }
    }
}
