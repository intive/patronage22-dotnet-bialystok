namespace Patronage.Contracts.ModelDtos.Projects
{
    public class PartialProjectDto
    {
        //public PropInfo<string> Alias { get; set; }
        //public PropInfo<string> Name { get; set; }
        //public PropInfo<string> Description { get; set; }
        //public PropInfo<bool> IsActive { get; set; } // should i can change it's value by PATCH action or only by DELETE ?

        public string? Alias { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
