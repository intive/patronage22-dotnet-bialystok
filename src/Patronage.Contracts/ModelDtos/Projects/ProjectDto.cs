namespace Patronage.Contracts.ModelDtos.Projects
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Alias { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}