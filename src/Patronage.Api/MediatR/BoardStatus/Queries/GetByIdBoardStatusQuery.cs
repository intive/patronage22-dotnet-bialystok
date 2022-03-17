using MediatR;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.BoardStatus.Queries
{
    public record GetByIdBoardStatusQuery(int BoardId, int StatusId) : IRequest<IEnumerable<BoardStatusDto>>;
}