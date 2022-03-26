using Patronage.Contracts.ModelDtos.Boards;
using Patronage.Models;

namespace Patronage.Contracts.Helpers
{
    public static class BoardQueryExtensions
    {
        public static IQueryable<Board> FilterBy(this IQueryable<Board> value, FilterBoardDto filter)
        {
            if (!string.IsNullOrEmpty(filter.SearchPhrase))
            {
                value = value.Where(x => x.Alias.Contains(filter.SearchPhrase) || x.Name.Contains(filter.SearchPhrase) || x.Description!.Contains(filter.SearchPhrase));
            }
            if (filter.ProjectId.HasValue)
            {
                value = value.Where(x => x.ProjectId == filter.ProjectId);
            }

            return value;
        }
    }
}
