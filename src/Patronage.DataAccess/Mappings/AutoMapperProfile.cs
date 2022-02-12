using AutoMapper;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;

namespace Patronage.DataAccess.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Project, ProjectDto>();

            CreateMap<Issue, IssueDto>();

            CreateMap<Board, BoardDto>().ReverseMap();

            CreateMap<PartialBoardDto, Board>()               
                .ForMember(x => x.Name, y => { y.Condition(src => src.Name != null); y.MapFrom(z => z.Name.Data); })
                .ForMember(x => x.Description, y => { y.Condition(src => src.Description != null); y.MapFrom(z => z.Description.Data); })
                .ForMember(x => x.IsActive, y => { y.Condition(src => src.IsActive != null); y.MapFrom(z => z.IsActive.Data); });           
        }
    }
}
