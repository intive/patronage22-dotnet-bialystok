using Patronage.Common;
using Patronage.Models;

namespace Patronage.Contracts.ModelDtos.Boards
{
    public class BaseBoardDto : IEntity
    {
        public string Alias { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public DateTime CreatedOn { get; set; }

        public BaseBoardDto(Board board)
        {
            Alias = board.Alias;
            Name = board.Name;
            Description = board.Description;
            ProjectId = board.ProjectId;
            CreatedOn = board.CreatedOn;
        }

        public BaseBoardDto()
        {
        }
    }
}