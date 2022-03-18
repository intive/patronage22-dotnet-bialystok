using Patronage.Models;

namespace Patronage.Contracts.ModelDtos.Boards
{
    public class BoardDto
    {
        public int Id { get; set; }
        public string Alias { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public int StatusId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<BoardStatusDto>? BoardStatuses { get; set; }

        public BoardDto(Board board)
        {
            Id = board.Id;
            Alias = board.Alias;
            Name = board.Name;
            Description = board.Description;
            ProjectId = board.ProjectId;
            IsActive = board.IsActive;
            CreatedOn = board.CreatedOn;
            ModifiedOn = board.ModifiedOn;
            BoardStatuses = (List<BoardStatusDto>?)board.BoardStatuses;
        }

        public BoardDto()
        {
        }
    }
}