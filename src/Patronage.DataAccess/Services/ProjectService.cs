using Microsoft.EntityFrameworkCore;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Projects;
using Patronage.Models;

namespace Patronage.DataAccess.Services
{
    public class ProjectService : IProjectService
    {
        private readonly TableContext _dbContext;

        public ProjectService(TableContext context)
        {
            _dbContext = context;
        }

        public async Task<int> Create(CreateProjectDto projectDto)
        {
            Project newProject = new();

            newProject.Name = projectDto.Name;
            newProject.Alias = projectDto.Alias;
            newProject.Description = projectDto.Description;
            newProject.IsActive = projectDto.IsActive;

            await _dbContext.Projects.AddAsync(newProject);
            await _dbContext.SaveChangesAsync();

            return newProject.Id;
        }

        public async Task<IEnumerable<ProjectDto>> GetAll(string? searchedPhrase)
        {
            var projectsQueryable = _dbContext
                .Projects
                .AsQueryable();

            if (searchedPhrase != null)
            {
                projectsQueryable = projectsQueryable.Where(p => p.Name.Contains(searchedPhrase) ||
                                                       p.Alias.Contains(searchedPhrase) ||
                                                       (p.Description != null && p.Description.Contains(searchedPhrase)));
            }

            var projects = await projectsQueryable
                .Select(project => new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    Alias = project.Alias,
                    Description = project.Description,
                    IsActive = project.IsActive,
                    CreatedOn = project.CreatedOn,
                    ModifiedOn = project.ModifiedOn
                })
                .ToListAsync();

            return projects;
        }

        public async Task<ProjectDto?> GetById(int id)
        {
            var project = await _dbContext
                .Projects
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project is null)
            {
                return null;
            }

            ProjectDto projectDto = new()
            {
                Id = project.Id,
                Name = project.Name,
                Alias = project.Alias,
                Description = project.Description,
                IsActive = project.IsActive,
                CreatedOn = project.CreatedOn,
                ModifiedOn = project.ModifiedOn
            };

            return projectDto;
        }

        public async Task<bool> Update(int id, UpdateProjectDto projectDto)
        {
            var project = _dbContext
                .Projects
                .FirstOrDefault(p => p.Id == id);

            if (project is null) return false;

            project.Name = projectDto.Name;
            project.Alias = projectDto.Alias;
            project.Description = projectDto.Description;
            project.IsActive = projectDto.IsActive;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> LightUpdate(int id, PartialProjectDto projectDto)
        {
            var project = _dbContext
                .Projects
                .FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return false;
            }

            if (projectDto.Description == null)
            {
                project.Description = project.Description;
            }
            else if (projectDto.Description?.Data == null)
            {
                project.Description = null;
            }
            else
            {
                project.Description = projectDto.Description.Data;
            }

            project.Name = projectDto.Name?.Data ?? project.Name;
            project.Alias = projectDto.Alias?.Data ?? project.Alias;
            project.IsActive = projectDto.IsActive?.Data ?? project.IsActive;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var project = _dbContext
                .Projects
                .FirstOrDefault(p => p.Id == id);

            if (project is null) return false;

            project.IsActive = false;

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}