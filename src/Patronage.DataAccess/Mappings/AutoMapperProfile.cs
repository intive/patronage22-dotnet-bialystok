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
        }
    }
}
