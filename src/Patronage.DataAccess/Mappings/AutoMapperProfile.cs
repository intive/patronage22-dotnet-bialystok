using AutoMapper;
using Patronage.Contracts;
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
            CreateMap<BoardStatus, BoardStatusDto>().ReverseMap();
            


            CreateMap<Issue, IssueDto>()
                .ForMember(m => m.Alias.Data, c => c.MapFrom(s => s.Alias))
                .ForMember(m => m.Name.Data, c => c.MapFrom(s => s.Name))
                .ForMember(m => m.Description.Data, c => c.MapFrom(s => s.Description));
            CreateMap<BaseIssueDto, Issue>();

            CreateMap<Board, BoardDto>().ReverseMap();

            CreateMap<PartialBoardDto, Board>()               
                .ForMember(x => x.Name, y => { y.Condition(src => src.Name != null); y.MapFrom(z => z.Name.Data); })
                .ForMember(x => x.Description, y => { y.Condition(src => src.Description != null); y.MapFrom(z => z.Description.Data); })
                .ForMember(x => x.IsActive, y => { y.Condition(src => src.IsActive != null); y.MapFrom(z => z.IsActive.Data); });           
        }
    }
}
