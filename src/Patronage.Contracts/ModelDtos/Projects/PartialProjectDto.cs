namespace Patronage.Contracts.ModelDtos.Projects
{
    public class PartialProjectDto
    {
        public PropInfo<string>? Alias { get; set; }
        public PropInfo<string>? Name { get; set; }
        public PropInfo<string?>? Description { get; set; }
        public PropInfo<bool>? IsActive { get; set; }
    }
}
