namespace Patronage.Contracts.ModelDtos.Boards
{
    public class PartialBoardDto
    {
        public PropInfo<string>? Alias { get; set; }
        public PropInfo<string>? Name { get; set; }
        public PropInfo<string>? Description { get; set; }
        public PropInfo<int>? ProjectId { get; set; }
        public PropInfo<bool>? IsActive { get; set; }
    }
}