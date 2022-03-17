using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.BoardStatus.Queries
{
    public record GetAllBoardStatusQuery() : IRequest<IEnumerable<BoardStatusDto>>;
}