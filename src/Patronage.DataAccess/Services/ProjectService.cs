using AutoMapper;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos.Projects;
using Patronage.Contracts.ModelDtos;
using Patronage.Models;
using Microsoft.EntityFrameworkCore;

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
            Project newProject = new Project();

            newProject.Name = projectDto.Name;
            newProject.Alias = projectDto.Alias;
            newProject.Description = projectDto.Description;
            newProject.IsActive = projectDto.IsActive;

            await _dbContext.Projects.AddAsync(newProject);
            await _dbContext.SaveChangesAsync();

            return newProject.Id;
        }



        public async Task<IEnumerable<ProjectDto>> GetAll(string searchedPhrase)
        {
            var  projects = _dbContext
                .Projects
                .Where(p => searchedPhrase == null || (p.Name.Contains(searchedPhrase)) ||
                                                       p.Alias.Contains(searchedPhrase) ||
                                                       p.Description.Contains(searchedPhrase))
                .ToList();


            var projectsDto = new List<ProjectDto>();

            foreach (var project in projects)
            {
                projectsDto.Add(new ProjectDto
                {
                    Id = project.Id,
                    Name = project.Name,
                    Alias = project.Alias,
                    Description = project.Description,
                    IsActive = project.IsActive,
                    CreatedOn = project.CreatedOn,
                    ModifiedOn = project.ModifiedOn
                });
            }

            return projectsDto;
        }



        public async Task<ProjectDto> GetById(int id)
        {
            var project = _dbContext
                .Projects
                .FirstOrDefault(p => p.Id == id);

            if (project is null) return null;


            ProjectDto projectDto = new ProjectDto
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

            if (project is null) return false;

            //if (projectDto.Description.Data is null) project.Description = null;

            project.Name = projectDto.Name?.Data ?? project.Name;
            project.Alias = projectDto.Alias?.Data ?? project.Alias;
            project.Description = projectDto.Description?.Data ?? project.Description;
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
