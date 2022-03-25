﻿namespace Patronage.Contracts.ModelDtos.Projects
{
    public class CreateProjectDto
    {
        public string Alias { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}