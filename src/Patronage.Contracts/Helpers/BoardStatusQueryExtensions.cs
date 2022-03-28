using Patronage.Contracts.ModelDtos.BoardsStatus;
using Patronage.Models;

namespace Patronage.Contracts.Helpers
{
    public static class BoardStatusQueryExtensions
    {
        public static IQueryable<BoardStatus> FilterBy(this IQueryable<BoardStatus> value, FilterBoardStatusDto filter)
        {
            if (filter.BoardId.HasValue)
            {
                value = value.Where(x => x.BoardId == filter.BoardId);
            }
            if (filter.StatusId.HasValue)
            {
                value = value.Where(x => x.StatusId == filter.StatusId);
            }

            return value;
        }
    }
}
