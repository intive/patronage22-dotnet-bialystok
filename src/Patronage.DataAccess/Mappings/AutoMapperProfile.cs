using AutoMapper;
using Patronage.Models;
using Patronage.Contracts.ModelDtos.Board;

namespace Patronage.DataAccess.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Board, BoardDto>().ReverseMap();

            CreateMap<PartialBoardDto, Board>()
                .ForMember(x => x.Alias, y => { y.Condition(src => src.Alias != null); y.MapFrom(z => z.Alias!.Data); })
                .ForMember(x => x.Name, y => { y.Condition(src => src.Name != null); y.MapFrom(z => z.Name!.Data); })
                .ForMember(x => x.Description, y => { y.Condition(src => src.Description != null); y.MapFrom(z => z.Description!.Data); })
                .ForMember(x => x.ProjectId, y => { y.Condition(src => src.ProjectId != null); y.MapFrom(z => z.ProjectId!.Data); })
                .ForMember(x => x.IsActive, y => { y.Condition(src => src.IsActive != null); y.MapFrom(z => z.IsActive!.Data); });

            CreateMap<UpdateBoardDto, Board>();
        }
    }
}
